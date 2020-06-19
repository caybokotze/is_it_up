using System.Collections.Generic;

namespace IsItUpOrDown
{
    public class DbSeeder
    {
        public static void Seed()
        {
            if (DataAccess.GetSites().Count == 0)
            {
                var websiteCheckList = new List<Website>
                {
                    new Website()
                    {
                        Name = "Merge Africa",
                        Timeout = 15,
                        Url = "https://www.merge.africa"
                    },
                    new Website()
                    {
                        Name = "Arya Block",
                        Timeout = 15,
                        Url = "https://www.aryablock.com"
                    },
                    new Website()
                    {
                        Name = "Roag",
                        Timeout = 15,
                        Url = "https://www.roag.org"
                    },
                    new Website()
                    {
                        Name = "Roag SA",
                        Timeout = 15,
                        Url = "https://www.roag.co.za"
                    },
                    new Website()
                    {
                        Name = "Fresco Paradigm",
                        Timeout = 15,
                        Url = "https://www.frescoparadigm.com"
                    },
                    new Website()
                    {
                        Name = "Bluesight Digital",
                        Timeout = 15,
                        Url = "https://www.bluesight.digital"
                    },
                    new Website()
                    {
                        Name = "Onform",
                        Timeout = 15,
                        Url = "https://www.onform.co.za"
                    },
                    new Website()
                    {
                        Name = "Forms Control",
                        Timeout = 15,
                        Url = "https://www.formscontrol.co.za"
                    },
                    new Website()
                    {
                        Name = "Rosebank Wealth Group",
                        Timeout = 15,
                        Url = "https://www.rosebankwealthgroup.com"
                    }
                };
                
                foreach (var item in websiteCheckList)
                {
                    DataAccess.InsertSite(item);
                }
            }
        }
    }
}