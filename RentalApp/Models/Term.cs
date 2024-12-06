namespace RentalApp.Models
{
    public class Term
    {

        public int TermID { get; set; }
        public string TermName { get; set; }

        public Term(int termID, string termName)
        {
            TermID = termID;
            TermName = termName;
        }

    }
}
