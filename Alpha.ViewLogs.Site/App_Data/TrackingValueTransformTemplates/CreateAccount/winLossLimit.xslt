<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:xslLogFormat="urn:LogFormat">
  <xsl:output method="html" indent="no" />
  <xsl:template match="winlosslimit[@grouptype=155]">
    <tr class="headerDetail" onclick="toggle(this)">
      <td colspan="6">
        <span class="icon-chevron-down"></span>
        <xsl:value-of select="xslLogFormat:GetLogResource('WinLossLimit')" />
      </td>
    </tr>
    <tr>
      <td colspan="6">
        <table class="tblDesc">
          <tr style="text-align:center;" class="headerSportTypeDetail">
            <td></td>
            <td>
              <xsl:value-of select="xslLogFormat:GetLogResource('MaximumWin')" />
            </td>
            <td>
              <xsl:value-of select="xslLogFormat:GetLogResource('MaximumLoss')" />
            </td>
            <td>
              <xsl:value-of select="xslLogFormat:GetLogResource('DailyAutoReset')" />
            </td>
          </tr>
          <xsl:for-each select="r">
            <tr>
              <td class="colW1">
                <xsl:choose>
                  <xsl:when test="@sporttype=208 and @bettype=2101">
                    <xsl:value-of select="xslLogFormat:GetProductName(6)" />
                  </xsl:when>
                  <xsl:otherwise>
                    <xsl:value-of select="xslLogFormat:GetSportName(@sporttype)" />
                  </xsl:otherwise>
                </xsl:choose>
              </td>
              <td class="colW2">
                <xsl:value-of select="xslLogFormat:FormatWinLossLimit(@maxwinlimit)" />
              </td>
              <td class="colW2">
                <xsl:value-of select="xslLogFormat:FormatWinLossLimit(@maxlosslimit)" />
              </td>
              <td class="colW2">
                <xsl:value-of select="@isautoreset" />
              </td>
            </tr>
          </xsl:for-each>
        </table>
      </td>
    </tr>
  </xsl:template>
</xsl:stylesheet>