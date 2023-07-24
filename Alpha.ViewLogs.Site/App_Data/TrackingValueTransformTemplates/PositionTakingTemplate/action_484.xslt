<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:xslLogFormat="urn:LogFormat">
  <xsl:param name="IsInternal" select="0"></xsl:param>
  <xsl:output method="html" indent="no" />
  <xsl:template match="root">
    <table class="tblDesc">
      <tr class="headerDetail" style="text-align:center;">
        <td rowspan="2">
          <xsl:value-of select="xslLogFormat:GetLogResource('TemplateName')" />
        </td>
        <td colspan="4" style="text-align:center;">
          <xsl:value-of select="xslLogFormat:GetLogResource('SuperPT')" />
        </td>
        <td rowspan="2">
          <xsl:value-of select="xslLogFormat:GetLogResource('SelectedByDefault')" />
        </td>
        <td rowspan="2">
          <xsl:value-of select="xslLogFormat:GetLogResource('subaccname')" />
        </td>
        <td rowspan="2">
          <xsl:value-of select="xslLogFormat:GetLogResource('ip')" />
        </td>
      </tr>
      <tr class="headerSportTypeDetail border-last-item" style="text-align:center;">
        <xsl:for-each select="r">
          <td>
            <xsl:value-of select="xslLogFormat:GetSubProductGroupName(@subgroupid)" />
          </td>
        </xsl:for-each>
      </tr>
      <tr>
        <td class="colW1">
          <xsl:value-of select="@templatename" />
        </td>
        <xsl:for-each select="r">
          <td class="colW2">
            <xsl:value-of select="@pt" />
          </td>
        </xsl:for-each>
        <td class="colW2">
          <xsl:value-of select="@isdefault" />
        </td>
        <td class="colW2">
          <xsl:value-of select="@subaccname" />
        </td>
        <td class="colW2">
          <xsl:value-of select="xslLogFormat:FormatIP(@ip)" />
        </td>
      </tr>
    </table>
  </xsl:template>
</xsl:stylesheet>