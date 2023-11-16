class LineChart {

    xMin = 0;
    xMax = 0;
    yMin = 0;
    yMax = 0;

    pictureBox;
    _PADDING;
    _graphBitmap;

    constructor(pictureBox) {

        this._PADDING = 50;
        this._graphBitmap = pictureBox;

        this._graphBitmap.width = 1000;
        this._graphBitmap.height = 1000;

    }


drawChart(data) {
    let g = this._graphBitmap.getContext("2d");

    let width = 1000 - 2 * this.PADDING;
    let height = 1000 - 2 * this.PADDING;


    this.xMin = 0;
    this.xMax = Math.max(...data.map(list => list.length));
	this.yMax = Math.max(...data.map(list => list.length));
    this.yMin = -Math.max(...data.map(list => list.length));
	
	
    g.clearRect(0, 0, 1000, 1000);
    g.beginPath();

    g.moveTo(this.PADDING, this.PADDING + height / 2);
    g.lineTo(this.PADDING + width, this.PADDING + height / 2);
    g.moveTo(this.PADDING, this.PADDING + height);
    g.lineTo(this.PADDING, this.PADDING);
    g.strokeStyle = "black";
    g.lineWidth = 2;
    g.stroke();

    for (let i = this.xMin; i <= this.xMax; i++) {
        let x = this.PADDING + (i - this.xMin) * width / (this.xMax - this.xMin);
        g.moveTo(x, this.PADDING + height / 2 - 5);
        g.lineTo(x, this.PADDING + height / 2 + 5);
        g.stroke();
        g.fillText(i.toString(), x - 10, this.PADDING + height / 2 + 10);
    }

    for (let i = this.yMin; i <= this.yMax; i++) {
        let y = this.PADDING + height - (i - this.yMin) * height / (this.yMax - this.yMin);
        g.moveTo(this.PADDING - 5, y);
        g.lineTo(this.PADDING + 5, y);
        g.stroke();
        g.fillText(i.toString(), this.PADDING - 30, y - 10);
    }

    let colors = ["blue", "green", "red", "orange", "purple", "brown", "magenta", "cyan"];
    for (let i = 0; i < data.length; i++) {
        let dataList = data[i];
        if (dataList.length < 2) continue;
        

        let x1 = this.PADDING;
        let y1 = this.PADDING + height / 2 - dataList[0] * height / (this.yMax - this.yMin);
        let x2 = this.PADDING + (1 - this.xMin) * width / (this.xMax - this.xMin);
		let y2 = this.PADDING + height / 2 - dataList[0] * height / (this.yMax - this.yMin);
		
		g.strokeStyle = colors[i % colors.length];
		g.beginPath();
		g.moveTo(x1, y1);
		g.lineTo(x2, y2);
		g.stroke();

        for (let j = 1; j < dataList.length; j++) {
			x1 = x2;
			y1 = y2;
            x2 = this.PADDING + (j + 1 - this.xMin) * width / (this.xMax - this.xMin);
			y2 += -dataList[j] * height / (this.yMax - this.yMin);
			
			g.strokeStyle = colors[i % colors.length];
			g.beginPath();
			g.moveTo(x1, y1);
			g.lineTo(x2, y2);
			g.stroke();
        }
    }
}

drawChartCont(data) {
    let g = this._graphBitmap.getContext("2d");

    let width = 1000 - 2 * this.PADDING;
    let height = 1000 - 2 * this.PADDING;


    this.xMin = 0;
    this.xMax = Math.max(...data.map(list => list.length));
	this.yMax = Math.max(...data.map((list) => Math.max(...list)));
    this.yMin = 0;
	
	
    g.clearRect(0, 0, 1000, 1000);
    g.beginPath();

    g.moveTo(this.PADDING, this.PADDING + height);
    g.lineTo(this.PADDING + width, this.PADDING + height);
	
    g.moveTo(this.PADDING, this.PADDING + height);
    g.lineTo(this.PADDING, this.PADDING);
    g.strokeStyle = "black";
    g.lineWidth = 2;
    g.stroke();

    for (let i = this.xMin; i <= this.xMax; i++) {
        let x = this.PADDING + (i - this.xMin) * width / (this.xMax - this.xMin);
        g.moveTo(x, this.PADDING + height - 5);
        g.lineTo(x, this.PADDING + height + 5);
        g.stroke();
        g.fillText(i.toString(), x - 10, this.PADDING + height + 10);
    }

    for (let i = this.yMin; i <= this.yMax; i++) {
        let y = this.PADDING + height - (i - this.yMin) * height / (this.yMax - this.yMin);
        g.moveTo(this.PADDING - 5, y);
        g.lineTo(this.PADDING + 5, y);
        g.stroke();
        g.fillText(i.toString(), this.PADDING - 30, y - 10);
    }

    let colors = ["blue", "green", "red", "orange", "purple", "brown", "magenta", "cyan"];
    for (let i = 0; i < data.length; i++) {
        let dataList = data[i];
        if (dataList.length < 2) continue;
        

        let x1 = this.PADDING;
        let y1 = this.PADDING + height / 2 - dataList[0] * height / (this.yMax - this.yMin);
        let x2 = this.PADDING + (1 - this.xMin) * width / (this.xMax - this.xMin);
		let y2 = this.PADDING + height / 2 - dataList[0] * height / (this.yMax - this.yMin);
		
		g.strokeStyle = colors[i % colors.length];
		g.beginPath();
		g.moveTo(x1, y1);
		g.lineTo(x2, y2);
		g.stroke();

        for (let j = 1; j < dataList.length; j++) {
			
			let x1 = this.PADDING + (j - 1 - this.xMin) * width / (this.xMax - this.xMin);
			let y1 = this.PADDING + height - (dataList[j - 1] - this.yMin) * height / (this.yMax - this.yMin);
			let x2 = this.PADDING + (j - this.xMin) * width / (this.xMax - this.xMin);
			let y2 = this.PADDING + height - (dataList[j] - this.yMin) * height / (this.yMax - this.yMin);

			g.strokeStyle = colors[i % colors.length];
			g.beginPath();
			g.moveTo(x1, y1);
			g.lineTo(x2, y2);
			g.stroke();
        }
    }
}

    get PADDING() {
        return this._PADDING;
    }

    set PADDING(value) {
        this._PADDING = value;
    }

    get pictureBox(){
        return this.pictureBox;
    }

    get _graphBitmap(){
        return this._graphBitmap;
    }
	
}

