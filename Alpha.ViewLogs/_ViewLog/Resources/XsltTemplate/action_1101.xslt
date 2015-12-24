<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:xslLogFormat="urn:LogFormat">
  <xsl:param name="IsInternal" select="1"></xsl:param>
  <xsl:output method="html" indent="no" />

  <xsl:template match="acc_customerinsmember_sag">
    <table class="tblDesc">
      <tr>
        <td class="colW1">
          <xsl:value-of select="xslLogFormat:GetLogResource('suspended')" />
        </td>
        <td class="colW2">
          <xsl:value-of select="@suspended" />
        </td>
        <td class="colW1">
          <xsl:value-of select="xslLogFormat:GetLogResource('closed')" />
        </td>
        <td class="colW2">
          <xsl:value-of select="@closed" />
        </td>
        <td class="colW1">
          <xsl:value-of select="xslLogFormat:GetLogResource('DisableHorseRacing')" />
        </td>
        <td class="colW2">
          <xsl:value-of select="@disablehorseracing" />
        </td>
      </tr>
      <tr>
        <td class="colW1">
          <xsl:value-of select="xslLogFormat:GetLogResource('DisableOT')" />
        </td>
        <td class="colW2">
          <xsl:value-of select="@disableot" />
        </td>
        <td class="colW1">
          <xsl:value-of select="xslLogFormat:GetLogResource('Disable151_51')" />
        </td>
        <td class="colW2">
          <xsl:value-of select="@disable151_51" />
        </td>
        <td class="colW1">
          <xsl:value-of select="xslLogFormat:GetLogResource('Disable152_41')" />
        </td>
        <td class="colW2">
          <xsl:value-of select="@disable152_41" />
        </td>
      </tr>
      <tr>
        <td class="colW1">
          <xsl:value-of select="xslLogFormat:GetLogResource('Disable152_51')" />
        </td>
        <td class="colW2">
          <xsl:value-of select="@disable152_51" />
        </td>
        <td class="colW1">
          <xsl:value-of select="xslLogFormat:GetLogResource('Disable153_51')" />
        </td>
        <td class="colW2">
          <xsl:value-of select="@disable153_51" />
        </td>
        <td class="colW1">
          <xsl:value-of select="xslLogFormat:GetLogResource('DisableOutright')" />
        </td>
        <td class="colW2">
          <xsl:value-of select="@disableoutright" />
        </td>
      </tr>
      <tr>
        <td class="colW1">
          <xsl:value-of select="xslLogFormat:GetLogResource('disableLiveCS')" />
        </td>
        <td class="colW2">
          <xsl:value-of select="@disable162_1101" />
        </td>
        <td class="colW1">
          <xsl:value-of select="xslLogFormat:GetLogResource('DisableCasino')" />
        </td>
        <td class="colW2">
          <xsl:value-of select="@disablecasino" />
        </td>
        <td class="colW1">
          <xsl:value-of select="xslLogFormat:GetLogResource('DisableAutoMPT')" />
        </td>
        <td class="colW2">
          <xsl:value-of select="@disableautompt" />
        </td>
      </tr>
      <tr>
        <td class="colW1">
          <xsl:value-of select="xslLogFormat:GetLogResource('IsInternal')" />
        </td>
        <td class="colW2">
          <xsl:value-of select="@isinternal" />
        </td>

        <td class="colW1">
          <xsl:value-of select="xslLogFormat:GetLogResource('disablehrfixedodds')" />
        </td>
        <td class="colW2">
          <xsl:value-of select="@disablehrfixedodds" />
        </td>
        <td class="colW1">
          <xsl:value-of select="xslLogFormat:GetLogResource('DisabledESports')" />
        </td>
        <td class="colW2">
          <xsl:value-of select="@disableesports" />
        </td>
      </tr>

      <tr>
        <td class="colW1">
          <xsl:value-of select="xslLogFormat:GetLogResource('DisableNumber')" />
        </td>
        <td class="colW2">
          <xsl:value-of select="@disablenumber" />
        </td>
        <td class="colW1">
          <xsl:value-of select="xslLogFormat:GetLogResource('DisableBingo')" />
        </td>
        <td class="colW2">
          <xsl:value-of select="@disablebingo" />
        </td>
        <td class="colW1">
          <xsl:if test="xslLogFormat:IsVisible('180') = 'True'">
            <xsl:value-of select="xslLogFormat:GetLogResource('DisabledVS')" />
          </xsl:if>
        </td>
        <td class="colW2">
          <xsl:if test="xslLogFormat:IsVisible('180') = 'True'">
            <xsl:value-of select="@disablevirtualsports" />
          </xsl:if>
        </td>
      </tr>
      <xsl:if test="xslLogFormat:IsVisibleKeno(@hidekeno) = 'True'">
        <tr >
          <td class="colW1">
            <xsl:value-of select="xslLogFormat:GetDisabledKenoBettypeName('1501')" />
          </td>
          <td class="colW2">
            <xsl:value-of select="@disable202_1501" />
          </td>
          <td class="colW1">
            <xsl:value-of select="xslLogFormat:GetDisabledKenoBettypeName('1503')" />
          </td>
          <td class="colW2">
            <xsl:value-of select="@disable202_1503" />
          </td>
          <td class="colW1">
            <xsl:value-of select="xslLogFormat:GetDisabledKenoBettypeName('1504')" />
          </td>
          <td class="colW2">
            <xsl:value-of select="@disable202_1504" />
          </td>
        </tr>
        <tr >
          <td class="colW1">
            <xsl:value-of select="xslLogFormat:GetDisabledKenoBettypeName('1505')" />
          </td>
          <td class="colW2">
            <xsl:value-of select="@disable202_1505" />
          </td>
          <td class="colW1">
            <xsl:value-of select="xslLogFormat:GetDisabledKenoBettypeName('1507')" />
          </td>
          <td class="colW2">
            <xsl:value-of select="@disable202_1507" />
          </td>
          <td class="colW1">
            <xsl:value-of select="xslLogFormat:GetDisabledKenoBettypeName('1508')" />
          </td>
          <td class="colW2">
            <xsl:value-of select="@disable202_1508" />
          </td>
        </tr>
        <tr >
          <td class="colW1">
            <xsl:value-of select="xslLogFormat:GetDisabledKenoBettypeName('1509')" />
          </td>
          <td class="colW2">
            <xsl:value-of select="@disable202_1509" />
          </td>
          <td class="colW1">
            <xsl:value-of select="xslLogFormat:GetDisabledKenoBettypeName('1511')" />
          </td>
          <td class="colW2">
            <xsl:value-of select="@disable202_1511" />
          </td>
          <td class="colW1">
            <xsl:value-of select="xslLogFormat:GetDisabledKenoBettypeName('1513')" />
          </td>
          <td class="colW2">
            <xsl:value-of select="@disable202_1513" />
          </td>
        </tr>
        <tr >
          <td class="colW1">
            <xsl:value-of select="xslLogFormat:GetDisabledKenoBettypeName('1514')" />
          </td>
          <td class="colW2">
            <xsl:value-of select="@disable202_1514" />
          </td>
          <td class="colW1">            
          </td>
          <td class="colW2">            
          </td>
          <td class="colW1">            
          </td>
          <td class="colW2">            
          </td>
        </tr>
      </xsl:if>

    </table>
  </xsl:template>
</xsl:stylesheet>
