namespace Alpaki.Moto.Database.ValueObjects
{
    public class BrandId
    {
        public long Value { get; private set; }

        private BrandId() { }

        public BrandId(long brandId)
        {
            Value = brandId;
        }

        public override bool Equals(object obj)
        {
            return Value.Equals(obj);
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        public override string ToString()
        {
            return Value.ToString();
        }

        public static bool operator ==(BrandId left, BrandId right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(BrandId left, BrandId right)
        {
            return !Equals(left, right);
        }

        public static implicit operator long(BrandId d) => d.Value;
    }
}
