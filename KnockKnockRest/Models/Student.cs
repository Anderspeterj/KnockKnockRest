namespace KnockKnockRest.Models
{
    public class Student
    {
        public int Id { get; set; }
        public int QrCode { get; set; }
        public string? Name { get; set; }
        public string? Address { get; set; }

        static bool IsEightDigits(int num)
        {
            int length = (int)Math.Floor(Math.Log10(num) + 1);
            return length == 8;
        }

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
    }


    
}
