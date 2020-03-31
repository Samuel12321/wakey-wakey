namespace Wakey_Wakey.Classes
{
    public struct NetworkDevice
    {
        public int id { get; set; }
        public int port { get; set; }
        public string name { get; set; }
        public string ip { get; set; }
        public string mac { get; set; }
        public string subnet { get; set; }
        public string adapter { get; set; }
        public bool status { get; set; }
    }
}
