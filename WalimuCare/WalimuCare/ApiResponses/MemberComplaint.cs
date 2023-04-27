using System;
using System.Collections.Generic;
using System.Text;

namespace WalimuCare.ApiResponses
{
    public class MemberComplaint
    {
        public string complaintTopicId { get; set; }
        public string complaintDescription { get; set; }
       // public string topic { get; set; }
        public DateTime dateSubmitted { get; set; }
        public string status1 { get; set; }
        public string statusColor
        {
            get
            {
                if (Status == 0)
                {
                    return "#EA212A";
                }
                else if (Status == 1)
                {
                    return "Green";
                }
                else
                {
                    return "Yellow";
                }
            }
        }

        public string StatusDescription
        {
            get
            {
                if (Status == 0)
                {
                    return "Open";
                }
                else if (Status == 1)
                {
                    return "Closed";
                }
                else
                {
                    return "Pending";
                }
            }
        }

        public string Id { get; set; }
        public string MemberNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Description { get; set; }
        public object HospitalName { get; set; }
        public object HospitalDepartment { get; set; }
        public string ComplaintType { get; set; }
        public string DependentName { get; set; }
        public string Topic { get; set; }
        public int Status { get; set; }
        public DateTime CreateDate { get; set; }

    }
}
