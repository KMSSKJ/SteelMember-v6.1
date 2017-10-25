
function formatMoney(mnum)
{
	var mnum = parseFloat(mnum);
	var strOutput="",strTemp="",strInTemp='';
	var unitArray = new Array("圆万亿","仟佰拾","零壹贰叁肆伍陆柒捌玖");
	var mnumArray = mnum.toString().split('.');
	var integralnum = mnumArray[0];
	var integrallen = integralnum.length;
	var decimalnum = (mnum.toString().indexOf('.')>=0) ? mnumArray[1].substr(0, 2) : '0';
	var decimallen = decimalnum.length;
	var ints = parseInt(integrallen/4);
	var inty = integrallen%4;
	if (ints>3 || (ints==3 && inty>0)) return "金额大写转换超出范围";
	if (inty>0)
	{
        ints++;
    integralnum = "0000".substr(inty)+integralnum;
		integrallen = integralnum.length;
	}
	var i = 0;
	while (i<integrallen)
    {
		var strOutTemp = "";
		strTemp = integralnum.substr(i, 4);
		i += 4;
		for (var j=0; j<4; j++)
		{
        strInTemp = parseInt(strTemp.substr(j, 1));
    strOutTemp += unitArray[2].substr(strInTemp, 1);
			if (strInTemp>0 && j<3) strOutTemp += unitArray[1].substr(j, 1);
		}
		strOutTemp = strOutTemp.replace(/零+$/, "");
		ints--;
		if (strOutTemp!="") strOutTemp += unitArray[0].substr(ints, 1);
		if (strTemp.substr(3,1)=='0') strOutTemp += "零";
		strOutput += strOutTemp;
	}
	strOutput = strOutput.replace(/零+/g, "零").replace(/^零/, "").replace(/零$/, "");
	if (strOutput=="圆") strOutput = "";
	if (decimallen==2)
	{
        strOutput += (decimalnum.charAt(0) != '0') ? unitArray[2].substr(parseInt(decimalnum.charAt(0)), 1) + "角" : "零";
    if (strOutput=="零") strOutput = "";
		strOutput += unitArray[2].substr(parseInt(decimalnum.charAt(1)), 1)+"分";
	}
	else
	{
		if (decimalnum!='0') strOutput += unitArray[2].substr(parseInt(decimalnum), 1)+"角";
		if (strOutput!="") strOutput += "整";
	}
	if (strOutput=="") strOutput = "金额为零";
	return strOutput;
}
