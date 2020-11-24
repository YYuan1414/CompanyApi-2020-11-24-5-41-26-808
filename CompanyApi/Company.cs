using System;

namespace CompanyApi
{
    public class Company
    {
        private string companyGuid;
        public Company()
        {
        }

        public Company(string name)
        {
            this.Name = name;
            this.CompanyId = Guid.NewGuid().ToString();
        }

        public string Name { get; set; }

        public string CompanyId
        { get; set; }

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            if (obj.GetType() != this.GetType())
            {
                return false;
            }

            return Equals((Company)obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        protected bool Equals(Company other)
        {
            return Name == other.Name && CompanyId == other.CompanyId;
        }
    }
}