namespace ExandasOracle.Domain
{
    public class Criteria
    {
        public string Text { get; set; }

        public object Entity { get; set; }

        public bool HasText
        {
            get
            {
                return Text != null && !Text.Equals(string.Empty);
            }
        }

        public string Pattern
        {
            get
            {
                return string.Format("%{0}%", Text);
            }
        }

    }
}
