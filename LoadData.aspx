<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LoadData.aspx.cs" Inherits="LoadData" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        Password: <input type="password" name="pwd" />
        <br />
        Season: 
        <select name="season">
            <option value="20072008">07-08</option>
            <option value="20082009">08-09</option>
            <option value="20092010">09-10</option>
            <option value="20102011">10-11</option>
        </select>
        <br />
        From Game Number: <input name="from" />
        <br />
        To Game Number: <input name="to" />
        <input type="submit" value="Load Games" />
    </div>
    </form>
</body>
</html>
