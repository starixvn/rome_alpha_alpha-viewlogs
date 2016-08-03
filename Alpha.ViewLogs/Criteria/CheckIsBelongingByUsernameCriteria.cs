namespace Alpha.ViewLogs.Criteria
{
    using Fanex.Data.Repository;

    public class CheckIsBelongingByUsernameCriteria : CriteriaBase
    {
        public string Username { get; set; }

        public int UplineId { get; set; }

        public int CustId { get; set; }

        public int IsSubAcc { get; set; }

        public override string GetSettingKey()
        {
            return "Age_CustomerSelCheckBelonging_ByUsername";
        }
    }
}
