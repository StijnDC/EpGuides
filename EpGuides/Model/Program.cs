using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EpGuides
{
    class Program
    {

        public string Title { get; set; }
        public string Directory { get; set; }
        public string TvRage { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string NumberOfEpisodes { get; set; }
        public string RunTime { get; set; }
        public string Network { get; set; }
        public string Country { get; set; }
  


        public Program(string title, string directory, string tvrage, string startDate, string endDate, string epnum, string runtime, string network, string country) {

            //string sDate = startDate;
            //DateTimeFormatInfo dtfi = new DateTimeFormatInfo();
            //dtfi.ShortDatePattern = "MMMM-yyyy";
            //DateTime objDate = Convert.ToDateTime(sDate);
            //Console.WriteLine(objDate);


            //dit moet nog nagezien worden!

           

            this.Title = title.Trim('"');
            this.Directory = directory;
            this.TvRage = tvrage;
            this.StartDate = startDate;
            this.EndDate = endDate;
           this.NumberOfEpisodes = epnum;
            this.RunTime = runtime;
            this.Network = network;
            this.Country = country;
           
    }


        public static List<Program> InlezenCSV(string file)
        {
            //resultaatlijst waarin alle goedgekeurde vogels (4 stukken) inkomen
            List<Program> resultaat = new List<Program>();

            using (StreamReader str = new StreamReader(file))
            {
                //De eerste lijn wordt overgeslagen omdat het de titel bevat
                string lijn = str.ReadLine();
                lijn = str.ReadLine();

                while (lijn != null)
                {
                    //lijn in stukken verdelen op de ';'
                    string[] stukken = lijn.Split(',');
                    try
                    {
                        if (stukken.Length == 9)
                        {
                            Program prog = new Program(stukken[0], stukken[1], stukken[2], stukken[3], stukken[4], stukken[5], stukken[6], stukken[7], stukken[8]);
                            resultaat.Add(prog);
                        }

                        else
                        {
                            
                        }
                    }
                    catch 
                    {
                       
                    }
                    
                    lijn = str.ReadLine();
                }

                
                return resultaat;

            }
        }


       

        public override string ToString()
        {

            return Title;

        
        }


        internal static Program GetProgramByName(object p)
        {
           
            return null;
        }
    }

}
