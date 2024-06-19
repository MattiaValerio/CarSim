namespace CarSim.BackEnd.Models
{
    public class CarDto
    {
        public string Plate { get; set; } = string.Empty;
        public int Wheels { get; } = 4; // 4 wheels
        public string Engine { get; set; } = string.Empty;
        public string Body { get; set; } = string.Empty; // material of the car body
        public int SteeringWheel { get; set; } // degrees of steering wheel
        public bool BrakePedal { get; set; } // when true, car is braking
        public string Tank { get; set; } = string.Empty; // percentage of fuel
        public string FuelType { get; set; } = string.Empty;// type of fuel 
        public bool Accelerator { get; set; } // when true, car is accelerating
        public string Type { get; set; } = string.Empty;
        public string Horn { get; set; } = string.Empty;
        public string Speed { get; set; } = string.Empty;
    }
}
