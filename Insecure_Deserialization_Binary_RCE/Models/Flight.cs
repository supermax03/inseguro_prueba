using System;

namespace Models
{
    [Serializable]
    public class Flight
    {
        public string Name { get; set; }
        public string From { get; set; }
        public string To { get; set; }
    }
}
