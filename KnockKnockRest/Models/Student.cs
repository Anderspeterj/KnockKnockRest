namespace KnockKnockRest.Models
{
    public class Student
    {
        public int Id { get; set; }
        public int QrCode { get; set; }
        public string? Name { get; set; }
        public string? Address { get; set; }


      

        public void ValidateName()
        {
            if (Name == null)
            {
                throw new ArgumentNullException("Name cannot be null");
            }
            if (Name.Length < 2)
            {
                throw new ArgumentOutOfRangeException("Name must be more than 1 character");
            }
            if (Name.Length > 100)
            {
                throw new ArgumentOutOfRangeException("Name cant be over 100 characters");
            }
        }

        public void ValidateQrCode()
        {
            if (QrCode == 0)
            {
                throw new ArgumentNullException("QrCode cannot be null");
            }
            if (QrCode > 99999999 || QrCode < 10000000)
            {
                throw new ArgumentException("QrCode must have exactly 8 digits");
            }
            
        }

        public void ValidateAddress()
        {
            if (Address == null)
            {
                throw new ArgumentNullException("Address cannot be null");
            }
            if(Address.Length < 2)
            {
                throw new ArgumentException("Address must be over 1 character");
            }
            if (Address.Length > 100)
            {
                throw new ArgumentException("Address cant be over 100 characters");
            }
        }
        public void Validate()
        {
            ValidateAddress();
            ValidateName();
            ValidateQrCode();
        }
        public override string ToString()
        {
            return $"Id: {Id}, Name: {Name}, QrCode: {QrCode}, Address: {Address}";
        }
    }


    
}
