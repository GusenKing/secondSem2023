using DNF;


var d = new DNF.DNF("x1&x2vx3");



var d1 = new DNF.DNF("X1&X2&X3VX2&-X3");
var d2 = new DNF.DNF("-X1&-X2&X3VX2&-X3");

var dt = new DNF.DNF("x1&x2&x3&x4vx1&x2vx1&x2vx1&x2&x3&x4&x5vx1vx3&x4");

dt.SortByLength();
Console.WriteLine(dt);