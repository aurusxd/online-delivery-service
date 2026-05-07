using DeliveryService.Models;
using DeliveryService.Repositories;

namespace DeliveryService.Services
{
    /// <summary>
    /// Объект для отображения
    /// </summary>
    public class CourierDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CourierPhone { get; set; }
        public bool IsActive { get; set; }
        public double Current_Lat { get; set; }
        public double Current_Lon { get; set; }
        public string Vehicle_Type { get; set; }
        public DateTime Created_At { get; set; }


        public CourierDto(int id, string name, int courierPhone, bool isActive, 
            double current_Lat, double current_Lon, string vehicle_Type, DateTime created_At)
        {
            Id = id;
            Name = name;
            CourierPhone = courierPhone;
            IsActive = isActive;
            Current_Lat = current_Lat;
            Current_Lon = current_Lon;
            Vehicle_Type = vehicle_Type;
            Created_At = created_At;
        }
    }

    /// <summary>
    /// Сервис, работающий с Курьерами
    /// </summary>
    public class CourierService
    {
        private readonly OrderRepository _orderRepository;
        private readonly CourierRepository _courierRepository;


        public CourierService(OrderRepository orderRepo, CourierRepository courirerRepo)
        {
            _orderRepository = orderRepo;
            _courierRepository = courirerRepo;
        }


        /// <summary>
        /// Создание DTO из объекта Courier
        /// </summary>
        /// <param name="c">Курьер</param>
        /// <returns>DTO курьера</returns>
        private CourierDto CreateDto(Courier c)
        {
            return new CourierDto(
                c.Id,
                c.Nanme,
                c.CourierPhone,
                c.IsActive,
                c.Current_Lat,
                c.Current_Lon,
                c.Vehicle_Type,
                c.Created_At
            );
        }

        /// <summary>
        /// Создание DTO списка из списка Courier
        /// </summary>
        /// <param name="couriers">Список курьеров</param>
        /// <returns>Список DTO курьеров</returns>
        private List<CourierDto> CreateDTOList(List<Courier> couriers)
        {
            List<CourierDto> c = new List<CourierDto>();
            foreach (var courier in couriers)
                c.Add(CreateDto(courier));

            return c;
        }

        /// <summary>
        /// Получение всех курьеров
        /// </summary>
        /// <returns>Список DTO курьеров</returns>
        public async Task<List<CourierDto>> GetAllAsync()
        {
            var couriers = await _courierRepository.GetAllAsync();
            List<CourierDto> c = new List<CourierDto>();

            foreach (var courier in couriers)
                c.Add(CreateDto(courier));

            return c;
        }

        /// <summary>
        /// Получение всех активных курьеров
        /// </summary>
        /// <returns>Список DTO активных курьеров</returns>
        public async Task<List<CourierDto>> GetActiveCouriersAsync()
        {
            var couriers = await _courierRepository.GetActive();
            return CreateDTOList(couriers);
        }

        /// <summary>
        /// Получение всех свободных от заказов курьеров
        /// </summary>
        /// <returns>Список DTO свободных от заказов курьеров</returns>
        public async Task<List<CourierDto>> GetFreeCouriersAsync()
        {
            var couriers = await _courierRepository.GetFreeCouriers();
            return CreateDTOList(couriers);
        }

        /// <summary>
        /// Назначение курьера на заказ
        /// </summary>
        /// <param name="courierId">ID курьера</param>
        /// <param name="orderId">ID заказа</param>
        /// <returns>Прошла ли операция назначения</returns>
        public async Task<bool> AssignCourierToOrderAsync(int courierId, int orderId)
        {
            Courier? courier = await _courierRepository.GetById(courierId);
            if (courier == null)
                return false;

            Order? order = await _orderRepository.GetById(orderId);
            if (order == null) 
                return false;

            if (order.CourierId != null || order.CourierId == courierId)
                return false;

            order.CourierId = courierId;
            order.Status = "Assigned"; // Заменить на нужный
            await _orderRepository.UpdateAsync(order);

            return true;
        }
    }
}