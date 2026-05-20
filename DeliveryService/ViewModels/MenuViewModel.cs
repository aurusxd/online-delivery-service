using DeliveryService.Commands;
using DeliveryService.Models;
using DeliveryService.Services;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace DeliveryService.ViewModels
{
    /// <summary>
    /// Логика взаимодействия пользователя и базы данных с MenuView
    /// </summary>
    public class MenuViewModel : BaseViewModel
    {
        private readonly WindowsService _windowsService;

        private readonly FoodCategoryService _categoryService;
        private readonly FoodService _foodService;
        private readonly BasketService _basketService;

        /// <summary>
        /// Текущий пользователь
        /// </summary>
        private readonly int _currentUserId;

        /// <summary>
        /// Список категорий
        /// </summary>
        private ObservableCollection<Categories> _categories;
        /// <summary>
        /// Список всей еды
        /// </summary>
        private ObservableCollection<Food> _menuItems;
        /// <summary>
        /// Список корзины пользователя
        /// </summary>
        private ObservableCollection<Basket> _basketItems;
        /// <summary>
        /// Полная сумма
        /// </summary>
        private decimal _totalPrice;

        /// <summary>
        /// Список категорий
        /// </summary>
        public ObservableCollection<Categories> Categories
        {
            get => _categories;
            set => SetProperty(ref _categories, value);
        }
        /// <summary>
        /// Список всей еды
        /// </summary>
        public ObservableCollection<Food> MenuItems
        {
            get => _menuItems;
            set => SetProperty(ref _menuItems, value);
        }
        /// <summary>
        /// Список корзины пользователя
        /// </summary>
        public ObservableCollection<Basket> BasketItems
        {
            get => _basketItems;
            set => SetProperty(ref _basketItems, value);
        }
        /// <summary>
        /// Полная сумма
        /// </summary>
        public decimal TotalPrice
        {
            get => _totalPrice;
            set => SetProperty(ref _totalPrice, value);
        }

        /// <summary>
        /// Команда загрузки данных
        /// </summary>
        public ICommand LoadDataCommand {  get; }
        /// <summary>
        /// Команда загрузки еды по категории
        /// </summary>
        public ICommand LoadMenuItemsCommand { get; }
        /// <summary>
        /// Команда добавления в корзину
        /// </summary>
        public ICommand AddToBasketCommand { get; }
        /// <summary>
        /// Команда удаления объекта из корзины
        /// </summary>
        public ICommand RemoveFromBasketCommand { get; }


        public MenuViewModel(WindowsService windowsService,
            FoodCategoryService foodCategoryService, FoodService foodService, BasketService basketService)
        {
            _windowsService = windowsService;
            _categoryService = foodCategoryService;
            _foodService = foodService;
            _basketService = basketService;

            // Это для тестов. В будущем надо как-то получать его
            _currentUserId = 1;

            Categories = new ObservableCollection<Categories>();
            MenuItems = new ObservableCollection<Food>();
            BasketItems = new ObservableCollection<Basket>();

            LoadDataCommand = new RelayCommandAsync(
                execute: () => TryRunTaskAsync(LoadDataAsync, "Ошибка загрузки данных"),
                canExecute: () => !IsBusy
            );

            LoadMenuItemsCommand = new RelayCommandAsync(
                execute: async (parameter) =>
                {
                    if (parameter == null)
                        await TryRunTaskAsync(LoadMenuAsync, "Ошибка загрузки объектов еды");
                    else
                    {
                        int categoryId = Convert.ToInt32(parameter);
                        await TryRunTaskAsync(() => LoadMenuFromCategoryAsync(categoryId), "Ошибка загрузки еды данной категории");
                    }
                },
                canExecute: _ => !IsBusy
            );

            LoadDataCommand.Execute(null);
        }


        /// <summary>
        /// Загрузка всех категорий
        /// </summary>
        private async Task LoadCategoriesAsync()
        {
            var list = await _categoryService.GetAllAsync();

            Categories.Clear();
            foreach (var c in list) 
                Categories.Add(c);
        }
        /// <summary>
        /// Загрузка всех объектов еды
        /// </summary>
        private async Task LoadMenuAsync()
        {
            var items = await _foodService.GetAllAsync();

            MenuItems.Clear();
            foreach (var item in items) 
                MenuItems.Add(item);
        }
        /// <summary>
        /// Загрузка объектов еды по категории
        /// </summary>
        /// <param name="categoryId">ID категории</param>
        private async Task LoadMenuFromCategoryAsync(int categoryId)
        {
            var items = await _foodService.GetAllFromCategoryAsync(categoryId);

            MenuItems.Clear();
            foreach (var item in items)
                MenuItems.Add(item);
        }
        /// <summary>
        /// Загрузка данных
        /// </summary>
        private async Task LoadDataAsync()
        {
            await LoadCategoriesAsync();
            await LoadMenuAsync();
        }
    }
}