using System;
using System.Collections.Generic;
using System.Text;

namespace WalimuV2.ApiResponses
{
    public class MemberComplaint
    {
        public string complaintTopicId { get; set; }
        public string complaintDescription { get; set; }

        public string topic { get; set; }
        public DateTime dateSubmitted { get; set; }
        public string status { get; set; }

        public string statusColor
        {
            get
            {
                if (status != null)
                {
                    if (status.Trim().ToLower() == "closed")
                    {
                        return "Green";
                    }
                    else if (status.Trim().ToLower() == "open")
                    {
                        return "Red";
                    }
                    else
                    {
                        return "Yellow";
                    }
                }
                else
                {
                    return "";
                }
            }
        }
    }
}
