var c = document.getElementById("c");
var ctx = c.getContext("2d");

c.height = document.documentElement.scrollHeight;
c.width = window.innerWidth;

var characters = [0, 1]

var font_size = 10;
var columns = c.width/font_size; 
var drops = [];
for(var x = 0; x < columns; x++)
	drops[x] = 1; 

function draw()
{
	ctx.fillStyle = "rgba(0, 0, 0, 0.05)";
	ctx.fillRect(0, 0, c.width, c.height);
	ctx.fillStyle = "#0980CC"; 
	ctx.font = font_size + "px arial";
	for(var i = 0; i < drops.length; i++)
	{
    var text = characters[Math.floor(Math.random()*characters.length)];
		//x = i*font_size, y = value of drops[i]*font_size
		ctx.fillText(text, i*font_size, drops[i]*font_size);
		
		//sending the drop back to the top randomly after it has crossed the screen
		//adding a randomness to the reset to make the drops scattered on the Y axis
		if(drops[i]*font_size > c.height && Math.random() > 0.975)
			drops[i] = 0;
		
		//incrementing Y coordinate
		drops[i]++;
	}
}

setInterval(draw, 100);



