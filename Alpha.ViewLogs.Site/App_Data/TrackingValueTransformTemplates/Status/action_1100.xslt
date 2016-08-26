<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:xslLogFormat="urn:LogFormat">
  <xsl:param name="IsInternal" select="1"></xsl:param>
  <xsl:output method="html" indent="no" />

  <xsl:template match="acc_customerinsmember_com">
    <table class="tblDesc">
      <tr>
        <td class="colW1">
          <xsl:value-of select="xslLogFormat:GetLogResource('disablehorseracing')" />
        </td>
        <td class="colW2">
          <xsl:value-of select="@disablehorseracing" />
        </td>
        <td class="colW1">
          <xsl:value-of select="xslLogFormat:GetLogResource('disableOT')" />
        </td>
        <td class="colW2">
          <xsl:value-of select="@disableot" />
        </td>
        <td class="colW1">
          <xsl:value-of select="xslLogFormat:GetLogResource('disable151_51')" />
        </td>
        <td class="colW2">
          <xsl:value-of select="@disable151_51" />
        </td>
      </tr>
      <tr>
        <td class="colW1">
          <xsl:value-of select="xslLogFormat:GetLogResource('disable152_41')" />
        </td>
        <td class="colW2">
          <xsl:value-of select="@disable152_41" />
        </td>
        <td class="colW1">
          <xsl:value-of select="xslLogFormat:GetLogResource('disable152_51')" />
        </td>
        <td class="colW2">
          <xsl:value-of select="@disable152_51" />
        </td>
        <td class="colW1">
          <xsl:value-of select="xslLogFormat:GetLogResource('disable153_51')" />
        </td>
        <td class="colW2">
          <xsl:value-of select="@disable153_51" />
        </td>
      </tr>
      <tr>
        <td class="colW1">
          <xsl:value-of select="xslLogFormat:GetLogResource('disablehrfixedodds')" />
        </td>
        <td class="colW2">
          <xsl:value-of select="@disableHRFixedOdds" />
        </td>
        <td class="colW1">
          <xsl:value-of select="xslLogFormat:GetLogResource('DisabledESports')" />
        </td>
        <td class="colW2">
          <xsl:value-of select="@disableesports" />
        </td>
        <td class="colW1">
          <xsl:value-of select="xslLogFormat:GetLogResource('disablenumber')" />
        </td>
        <td class="colW2">
          <xsl:value-of select="@disablenumber" />
        </td>
      </tr>
      <tr>
        <td class="colW1">
          <xsl:value-of select="xslLogFormat:GetLogResource('disableCasino')" />
        </td>
        <td class="colW2">
          <xsl:value-of select="@disablecasino" />
        </td>
        <td class="colW1">
          <xsl:value-of select="xslLogFormat:GetLogResource('disablebingo')" />
        </td>
        <td class="colW2">
          <xsl:value-of select="@disablebingo" />
        </td>
        <td class="colW1">
          <xsl:value-of select="xslLogFormat:GetLogResource('isinternal')" />
        </td>
        <td class="colW2">
          <xsl:value-of select="@isinternal" />
        </td>
      </tr>
      <xsl:if test="xslLogFormat:IsVisibleKeno(@hidekeno) = 'True'">
        <tr >
          <td class="colW1">
            <xsl:value-of select="xslLogFormat:GetDisabledKenoBetTypeName('1501')" />
          </td>
          <td class="colW2">
            <xsl:value-of select="@disable202_1501" />
          </td>
          <td class="colW1">
            <xsl:value-of select="xslLogFormat:GetDisabledKenoBetTypeName('1503')" />
          </td>
          <td class="colW2">
            <xsl:value-of select="@disable202_1503" />
          </td>
          <td class="colW1">
            <xsl:value-of select="xslLogFormat:GetDisabledKenoBetTypeName('1504')" />
          </td>
          <td class="colW2">
            <xsl:value-of select="@disable202_1504" />
          </td>
        </tr>
        <tr >
          <td class="colW1">
            <xsl:value-of select="xslLogFormat:GetDisabledKenoBetTypeName('1505')" />
          </td>
          <td class="colW2">
            <xsl:value-of select="@disable202_1505" />
          </td>
          <td class="colW1">
            <xsl:value-of select="xslLogFormat:GetDisabledKenoBetTypeName('1507')" />
          </td>
          <td class="colW2">
            <xsl:value-of select="@disable202_1507" />
          </td>
          <td class="colW1">
            <xsl:value-of select="xslLogFormat:GetDisabledKenoBetTypeName('1508')" />
          </td>
          <td class="colW2">
            <xsl:value-of select="@disable202_1508" />
          </td>
        </tr>
        <tr >
          <td class="colW1">
            <xsl:value-of select="xslLogFormat:GetDisabledKenoBetTypeName('1509')" />
          </td>
          <td class="colW2">
            <xsl:value-of select="@disable202_1509" />
          </td>
          <td class="colW1">
            <xsl:value-of select="xslLogFormat:GetDisabledKenoBetTypeName('1515')" />
          </td>
          <td class="colW2">
            <xsl:value-of select="@disable202_1515" />
          </td>
          <td class="colW1">
          </td>
          <td class="colW2">
          </td>
        </tr>
      </xsl:if>
      <xsl:for-each select="@*">
        <xsl:value-of select="xslLogFormat:RenderCustStatus(name(), .)"/>
      </xsl:for-each>
    </table>
  </xsl:template>
</xsl:stylesheet>
