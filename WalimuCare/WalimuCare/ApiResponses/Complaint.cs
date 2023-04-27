namespace WalimuCare.ApiResponses
{
    public class Complaint
    {
        public string id { get; set; }
        public string topic { get; set; }
        public string hospitalName { get; set; }
        public string hospitalDepartment { get; set; }
        public string discription { get; set; }
        public string type { get; set; }

        public override string ToString()
        {
            return topic;
        }
    }
    public class ComplaintsSelectList
    {
        public string id { get; set; }
        public string topic { get; set; }
    }

}
