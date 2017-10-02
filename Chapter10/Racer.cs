using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chapter10
{
    [Serializable]
    public class Racer :IComparable<Racer>,IFormattable
    {
        public int Id { get; private set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Country { get; set; }
        public int Wins { get; set; }
        public Racer(int id,string firstName,string lastName,string country)
            : this(id,firstName,lastName,country,wins: 0)
        {

        }

        public Racer(int id, string firstName, string lastName, string country, int wins)
        {
            this.Id = id;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Country = country;
            this.Wins = wins;
        }

        public override string ToString()
        {
            return string.Format("{0} {1}", FirstName, LastName);
        }

        public string ToString(string format, IFormatProvider formatProvider)
        {
            if (format == null)
            {
                format = "N";
            }
            switch (format)
            {
                case "N":
                    return ToString();
                case "F":
                    return FirstName;
                case "L":
                    return LastName;
                case "W":
                    return string.Format("{0}, Wins {1}", ToString(), Wins);
                case "C":
                    return string.Format("{0}, Country: {1}", ToString(), Country);
                case "A":
                    return string.Format("{0}, {1} Wins: {2}", ToString(), Country, Wins);
                default:
                    throw new FormatException(string.Format(formatProvider,"Format {0} is not supported", format));
            }
        }

        public string ToString(string format)
        {
            return ToString(format, null);
        }

        public int CompareTo(Racer other)
        {
            if (other == null)
            {
                return -1;
            }

            int compare = string.Compare(this.LastName, other.LastName);
            if (compare == 0)
            {
                return string.Compare(this.FirstName, other.FirstName);
            }

            return compare;
        }
    }

    public class RacerComparer : IComparer<Racer>
    {
        public enum CompareType
        {
            FirstName,
            LastName,
            Country,
            Wins
        }

        private CompareType compareType;
        public RacerComparer(CompareType compareType)
        {
            this.compareType = compareType;
        }
        public int Compare(Racer x,Racer y)
        {
            if (x == null && y == null)
            {
                return 0;
            }

            if (x == null)
            {
                return -1;
            }

            if (y == null)
            {
                return 1;
            }

            int result;
            switch (compareType)
            {
                case CompareType.FirstName:
                    return string.Compare(x.FirstName, y.FirstName);
                case CompareType.LastName:
                    return string.Compare(x.LastName, y.LastName);
                case CompareType.Country:
                    result = string.Compare(x.Country, y.Country);
                    if (result == 0)
                    {
                        return string.Compare(x.LastName, y.LastName);
                    }
                    else
                    {
                        return result;
                    }
                case CompareType.Wins:
                    return x.Wins.CompareTo(y.Wins);
                default:
                    throw new ArgumentException("Invalid Compare Type");
                    
            }
        }
    }

    public class FindCountry
    {
        private string country;
        public FindCountry(string country)
        {
            this.country = country;
        }

        public bool FindCountryPredicate(Racer racer)
        {
            Contract.Requires<ArgumentNullException>(racer != null);
            return racer.Country == country;
        }
    }
}
