<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:xslLogFormat="urn:LogFormat">
    <xsl:param name="IsInternal" select="0"></xsl:param>
    <xsl:output method="html" indent="no" />
    <xsl:template match="r">
      <table class="tblDesc">
        <tr>
          <td class="colW1">
            <xsl:value-of select="xslLogFormat:GetLogResource('product')" />
          </td>
          <td class="colW2">
            <xsl:value-of select="xslLogFormat:GetProductName(@productid)" />
          </td>
          <td class="colW1">
            <xsl:value-of select="xslLogFormat:GetLogResource('subAccName')" />
          </td>
          <td class="colW2">
            <xsl:value-of select="@subaccname" />
          </td>
          <td class="colW1">
            <xsl:value-of select="xslLogFormat:GetLogResource('ip')" />
          </td>
          <td class="colW2">
            <xsl:value-of select="xslLogFormat:FormatIP(@ip)" />
          </td>
        </tr>
        <tr>
          <td class="colW1">
            <xsl:value-of select="xslLogFormat:GetLogResource('Sport')" /> - 
            <xsl:value-of select="xslLogFormat:GetLogResource('bettype')" />
          </td>
          <td class="colW2">
            <xsl:value-of select="xslLogFormat:GetSportName(@sporttype)" /> - 
            <xsl:value-of select="xslLogFormat:GetBetTypeName(@bettype)" />
          </td>
          <td class="colW1">
            <xsl:value-of select="xslLogFormat:GetLogResource('IsLive')" />
          </td>
          <td class="colW2">
            <xsl:value-of select="@islive" />
          </td>
          <td class="colW1">
            <xsl:value-of select="xslLogFormat:GetLogResource('MinPTAgent')" />
          </td>
          <td  class="colW2">
            <xsl:value-of select="@agentminpt" />
          </td>
        </tr>
      </table>
    </xsl:template>
</xsl:stylesheet>