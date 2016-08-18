namespace Alpha.ViewLogs.Services
{
    using Alpha.ViewLogs.Params;

    public interface XsltLogFormatter
    {
        void AssignParams(TransformParam transformParam);

        string FormatIP(string ip);

        string FormatCountryCode(string ip, string country);

        bool IsVisible(string key);

        string GetLogResource(string key);

        string GetSportName(int sportType);

        string GetBettypeName(int betType);

        string TransferConditionFromCode(string dayCodesWithDiff);

        string FormatBetTypeId(string betTypeId);

        string FormatErrCode(string code);

        string FormatErrMsg(string code);

        string FormatMemberWinLimit(string value);

        string FormatUnlimited(int unlimited);

        string GetSportWithConditionHtml(
            string disableVitualSports,
            string disableGoldDeluxe,
            string disableColossusBets,
            string disableCockfighting,
            string disableLiveCasinoAllbet,
            string disableLiveCasinoVoidbridge);

        bool IsShowMyanmarOdds();

        //void Set

        bool IsVisibleKeno(string hideKeno);

        string GetDisabledKenoBetTypeName(string disableBetType);

        bool IsRNGCasino(int sportType);

        bool IsGoldDeluxe(int sportType);
    }
}
