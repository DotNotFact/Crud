using MessageBoxButton = System.Windows.MessageBoxButton;
using MessageBoxResult = System.Windows.MessageBoxResult;
using MessageBoxImage = System.Windows.MessageBoxImage;
using MessageBox = System.Windows.MessageBox;
using Microsoft.EntityFrameworkCore;
using System.Windows.Input;
using Wpf.Ui.Controls;
using System.Windows;
using WpfApp1.Models;
using WpfApp1.Data;

namespace WpfApp1.Windows;

public partial class MainWindow : FluentWindow
{
    private readonly ApplicationDbContext _context;
    private Product? _selectedProduct;
    private List<Product>? _cachedProducts;

    public MainWindow(ApplicationDbContext context)
    {
        InitializeComponent();

        _context = context;
        LoadProducts();
    }

    private async void LoadProducts()
    {
        await LoadProductsAsync(forceReload: false);
    }

    private async Task LoadProductsAsync(bool forceReload = false)
    {
        try
        {
            if (!forceReload && _cachedProducts != null)
            {
                ProductsDataGrid.ItemsSource = _cachedProducts;
                ProductCountTextBlock.Text = $"Всего товаров: {_cachedProducts.Count}";
                StatusTextBlock.Text = "Готов к работе";
                return;
            }

            StatusTextBlock.Text = "Загрузка товаров...";
            
            var products = await _context.Products.AsNoTracking().ToListAsync();
            _cachedProducts = products;
            ProductsDataGrid.ItemsSource = products;
            ProductCountTextBlock.Text = $"Всего товаров: {products.Count}";
            StatusTextBlock.Text = "Готов к работе";
        }
        catch (Exception ex)
        {
            StatusTextBlock.Text = $"Ошибка загрузки: {ex.Message}";
            MessageBox.Show($"Ошибка при загрузке данных: {ex.Message}", "Ошибка", 
                MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    private void RefreshButton_Click(object sender, RoutedEventArgs e)
    { 
        _ = LoadProductsAsync(forceReload: true);
    }


    private void FormField_GotFocus(object sender, RoutedEventArgs e)
    {
        if (_selectedProduct == null)
        {
            StatusTextBlock.Text = "Режим создания нового товара";
        }
        else
        {
            StatusTextBlock.Text = $"Редактирование товара: {_selectedProduct.Name}";
        }
    }

    private void ProductsDataGrid_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
    {
        if (ProductsDataGrid.SelectedItem is Product product)
        {
            _selectedProduct = product;

            NameTextBox.Text = product.Name;
            DescriptionTextBox.Text = product.Description;
            PriceNumberBox.Value = (double)product.Price;
            QuantityNumberBox.Value = (double)product.Quantity;

            FormTitleTextBlock.Text = "Редактировать товар";
            SaveButton.Content = "Обновить";
            DeleteButton.IsEnabled = true;
            StatusTextBlock.Text = $"Выбран товар: {product.Name}";
        }
        else
        {
            DeleteButton.IsEnabled = false;
        }
    }

    private async void SaveButton_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            // Валидация
            if (string.IsNullOrWhiteSpace(NameTextBox.Text))
            {
                MessageBox.Show("Название товара обязательно для заполнения", "Ошибка валидации", 
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                NameTextBox.Focus();
                return;
            }

            if (PriceNumberBox.Value <= 0)
            {
                MessageBox.Show("Цена должна быть больше 0", "Ошибка валидации", 
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                PriceNumberBox.Focus();
                return;
            }

            StatusTextBlock.Text = "Сохранение...";

            if (_selectedProduct != null)
            {
                // Обновление через ExecuteUpdateAsync
                var rowsAffected = await _context.Products
                    .Where(p => p.Uid == _selectedProduct.Uid)
                    .ExecuteUpdateAsync(setters => setters
                        .SetProperty(p => p.Name, NameTextBox.Text)
                        .SetProperty(p => p.Description, DescriptionTextBox.Text ?? string.Empty)
                        .SetProperty(p => p.Price, (decimal)(PriceNumberBox.Value ?? 0))
                        .SetProperty(p => p.Quantity, (int)(QuantityNumberBox.Value ?? 0))
                    );

                if (rowsAffected > 0)
                {
                    StatusTextBlock.Text = "Товар обновлен успешно";
                    
                    // Обновляем кэш
                    var updatedProduct = new Product
                    {
                        Uid = _selectedProduct.Uid,
                        Name = NameTextBox.Text,
                        Description = DescriptionTextBox.Text ?? string.Empty,
                        Price = (decimal)(PriceNumberBox.Value ?? 0),
                        Quantity = (int)(QuantityNumberBox.Value ?? 0),
                        CreatedDate = _selectedProduct.CreatedDate
                    };
                    UpdateProductInCache(updatedProduct);
                }
                else
                {
                    MessageBox.Show("Не удалось обновить товар", "Ошибка", 
                        MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }
            else
            {
                // Создание нового товара
                var newProduct = new Product
                {
                    Uid = Guid.NewGuid(),
                    Name = NameTextBox.Text,
                    Description = DescriptionTextBox.Text ?? string.Empty,
                    Price = (decimal)(PriceNumberBox.Value ?? 0),
                    Quantity = (int)(QuantityNumberBox.Value ?? 0)
                };

                _context.Products.Add(newProduct);
                await _context.SaveChangesAsync();
                
                StatusTextBlock.Text = "Товар создан успешно";
                AddProductToCache(newProduct);
            }

            ClearForm();
        }
        catch (Exception ex)
        {
            StatusTextBlock.Text = $"Ошибка сохранения: {ex.Message}";
            MessageBox.Show($"Ошибка при сохранении: {ex.Message}", "Ошибка", 
                MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    private async void DeleteButton_Click(object sender, RoutedEventArgs e)
    {
        if (_selectedProduct == null) return;

        var result = MessageBox.Show(
            $"Вы уверены, что хотите удалить товар '{_selectedProduct.Name}'?",
            "Подтверждение удаления",
            MessageBoxButton.YesNo,
            MessageBoxImage.Question);

        if (result == MessageBoxResult.Yes)
        {
            try
            {
                StatusTextBlock.Text = "Удаление...";

                var existingProduct = await _context.Products.FindAsync(_selectedProduct.Uid);

                if (existingProduct != null)
                {
                    _context.Products.Remove(existingProduct);
                    await _context.SaveChangesAsync();

                    StatusTextBlock.Text = "Товар удален успешно";
                    RemoveProductFromCache(_selectedProduct.Uid);
                    ClearForm();
                }
                else
                {
                    MessageBox.Show("Товар не найден в базе данных", "Ошибка",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                StatusTextBlock.Text = $"Ошибка удаления: {ex.Message}";
                MessageBox.Show($"Ошибка при удалении: {ex.Message}", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }

    private void ClearButton_Click(object sender, RoutedEventArgs e)
    {
        ClearForm();
    }

    private void NewButton_Click(object sender, RoutedEventArgs e)
    {
        // Явный переход в режим создания нового товара
        _selectedProduct = null;
        ProductsDataGrid.SelectedItem = null;
        DeleteButton.IsEnabled = false;
        
        // Очищаем форму
        NameTextBox.Text = string.Empty;
        DescriptionTextBox.Text = string.Empty;
        PriceNumberBox.Value = 0;
        QuantityNumberBox.Value = 0;
        
        // Обновляем UI для режима создания
        FormTitleTextBlock.Text = "Добавить товар";
        SaveButton.Content = "Сохранить";
        
        StatusTextBlock.Text = "Режим создания нового товара";
        
        // Устанавливаем фокус на первое поле
        NameTextBox.Focus();
    }

    private void ClearForm()
    {
        _selectedProduct = null;
        NameTextBox.Text = string.Empty;
        DescriptionTextBox.Text = string.Empty;
        PriceNumberBox.Value = 0;
        QuantityNumberBox.Value = 0;
        ProductsDataGrid.SelectedItem = null;
        DeleteButton.IsEnabled = false;

        // Возвращаем UI в режим создания
        FormTitleTextBlock.Text = "Добавить товар";
        SaveButton.Content = "Сохранить";

        StatusTextBlock.Text = "Форма очищена";
    }

    private void UpdateStatus(string message, bool isLoading)
    {
        StatusTextBlock.Text = message;
    }

    // ОПТИМИЗАЦИЯ: Локальное обновление кэша для избежания ненужных запросов к БД
    private void UpdateProductInCache(Product updatedProduct)
    {
        if (_cachedProducts != null)
        {
            var existingIndex = _cachedProducts.FindIndex(p => p.Uid == updatedProduct.Uid);
            if (existingIndex >= 0)
            {
                _cachedProducts[existingIndex] = updatedProduct;
                ProductsDataGrid.ItemsSource = null;
                ProductsDataGrid.ItemsSource = _cachedProducts;
            }
        }
    }

    private void AddProductToCache(Product newProduct)
    {
        if (_cachedProducts != null)
        {
            _cachedProducts.Add(newProduct);
            ProductsDataGrid.ItemsSource = null;
            ProductsDataGrid.ItemsSource = _cachedProducts;
            ProductCountTextBlock.Text = $"Всего товаров: {_cachedProducts.Count}";
        }
    }

    private void RemoveProductFromCache(Guid uid)
    {
        if (_cachedProducts != null)
        {
            var removedCount = _cachedProducts.RemoveAll(p => p.Uid == uid);
            if (removedCount > 0)
            {
                ProductsDataGrid.ItemsSource = null;
                ProductsDataGrid.ItemsSource = _cachedProducts;
                ProductCountTextBlock.Text = $"Всего товаров: {_cachedProducts.Count}";
            }
        }
    }

    private void TitleBar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        if (e.LeftButton == MouseButtonState.Pressed)
        {
            this.DragMove();
        }
    }
}