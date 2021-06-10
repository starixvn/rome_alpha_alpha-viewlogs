<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:xslLogFormat="urn:LogFormat">
  <xsl:param name="IsInternal" select="0"></xsl:param>
  <xsl:output method="html" indent="no" />

  <xsl:template match="acc_custsetting_maxpayoutupd">
    <div class="headerSportTypeDetail">
      <xsl:value-of select="xslLogFormat:GetLogResource('UpdateParlayMaxPayoutIndirectly')" />
    </div>
    <table class="tblDesc">
      <tr>
        <td class="colW1">
          <xsl:value-of select="xslLogFormat:GetLogResource('SourceAccount')" />
        </td>
        <td class="colW2">
          <xsl:value-of select="@sourceaccount" />
        </td>
        <td class="colW1">
          <xsl:value-of select="xslLogFormat:GetLogResource('SoccerSMP')" />
        </td>
        <td class="colW2">
          <xsl:value-of select="@soccersinglematchparlay" />
        </td>
        <td class="colW1">
          <xsl:value-of select="xslLogFormat:GetLogResource('Soccer2')" />
        </td>
        <td class="colW2">
          <xsl:value-of select="@soccercombo2" />
        </td>
      </tr>

      <tr>
        <td class="colW1">
          <xsl:value-of select="xslLogFormat:GetLogResource('Soccer35')" />
        </td>
        <td class="colW2">
          <xsl:value-of select="@soccercombo35" />
        </td>
        <td class="colW1">
          <xsl:value-of select="xslLogFormat:GetLogResource('Soccer6pSP')" />
        </td>
        <td class="colW2">
          <xsl:value-of select="@soccercomboover6" />
        </td>
        <td class="colW1">
          <xsl:value-of select="xslLogFormat:GetLogResource('MixSportsSMP')" />
        </td>
        <td class="colW2">
          <xsl:value-of select="@mixsportssinglematchparlay" />
        </td>
      </tr>
      <tr>
        <td class="colW1">
          <xsl:value-of select="xslLogFormat:GetLogResource('MixSport2')" />
        </td>
        <td class="colW2">
          <xsl:value-of select="@mixsportscombo2" />
        </td>
        <td class="colW1">
          <xsl:value-of select="xslLogFormat:GetLogResource('MixSports35')" />
        </td>
        <td class="colW2">
          <xsl:value-of select="@mixsportscombo35" />
        </td>
        <td class="colW1">
          <xsl:value-of select="xslLogFormat:GetLogResource('MixSports6pSP')" />
        </td>
        <td class="colW2">
          <xsl:value-of select="@mixsportscomboover6" />
        </td>
      </tr>
      <tr>
        <td class="colW1">
          <xsl:value-of select="xslLogFormat:GetLogResource('ErrCode')" />
        </td>
        <td class="colW2">
          <xsl:value-of select="@errcode" />
        </td>
        <td class="colW1">
          <xsl:value-of select="xslLogFormat:GetLogResource('ErrMsg')" />
        </td>
        <td class="colW2">
          <xsl:value-of select="@errmsg" />
        </td>
        <td class="colW1">
          <xsl:value-of select="xslLogFormat:GetLogResource('IP')" />
        </td>
        <td class="colW2">
          <xsl:value-of select="xslLogFormat:FormatIP(@ip)" />
        </td>
      </tr>
    </table>
  </xsl:template>
</xsl:stylesheet>