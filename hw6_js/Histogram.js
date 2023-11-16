class Histogram {

    lineChart;
    data;
    k;
	additionalString;
	
	constructor(lineChart, data, k, additionalString) {
        this.lineChart = lineChart;
        this.data = data;
        this.k = k;

		if (typeof additionalString === 'string' && additionalString.trim() !== '') {
			this.additionalString = additionalString;
			this.DrawVerticalHistogramF();
        } else {
            this.additionalString = '';
			this.DrawVerticalHistogram();
        }
		
    }

    drawLine(ctx, x1, y1, x2,y2, stroke = 'black', width = 3) {
        // start a new path
        ctx.beginPath();

        // place the cursor from the point the line should be started 
        ctx.moveTo(x1, y1);

        // draw a line from current cursor position to the provided x,y coordinate
        ctx.lineTo(x2, y2);

        // set strokecolor
        ctx.strokeStyle = stroke;

        // set lineWidht 
        ctx.lineWidth = width;

        // add stroke to the line 
        ctx.stroke();
      }

    DrawVerticalHistogram() {
        let lineChartPadding = this.lineChart.PADDING;
        let width = 1000 - 2 * lineChartPadding;
        let height = 1000 - 2 * lineChartPadding;
        
        let yMin = this.lineChart.yMin;
        let yMax = this.lineChart.yMax;

        let dist = this.computeDist(this.data);

        let g = this.lineChart._graphBitmap.getContext('2d');

        // Draw the vertical Y-axis
        this.drawLine(g,lineChartPadding + width, lineChartPadding + height, lineChartPadding + width, lineChartPadding);

        let numIntervallo = 0;
        for (let i = yMin + (yMax - yMin) / this.k; i < yMax; i += (yMax - yMin) / this.k) {
            // Numbers
            let y = lineChartPadding + height - (i - yMin) * height / (yMax - yMin);
            this.drawLine(g,lineChartPadding + width - 5, y, lineChartPadding + width + 5, y);
            g.fillText(i.toString(), lineChartPadding + width + 5, y - 5);

            // Draw K rectangles
            let rect_len = (dist[numIntervallo] / Math.max(...dist)) * height;
            let interval_len = height / this.k;
            let x1 = lineChartPadding + width - rect_len;
            let y1 = lineChartPadding + height - (i - yMin) * height / (yMax - yMin);
            numIntervallo++;
			
			g.fillStyle = 'rgba(0, 0, 255, 0.3)'; // Blue with 30% opacity
			g.fillRect(x1, y1, rect_len, interval_len);
        }
    }

    computeDist(systemsFinalValues) {
        let dist = Array(this.k).fill(0);
        let lenInterval = 2 * this.lineChart.yMax / this.k;
        let j = this.lineChart.yMin;

        for (let numIntervallo = 0; numIntervallo < this.k; numIntervallo++) {
            for (let key in systemsFinalValues) {
                if (systemsFinalValues[key] >= j && systemsFinalValues[key] < j + lenInterval) {
                    dist[numIntervallo]++;
                }
            }
            j += lenInterval;
        }
        return dist;
    }
	
	DrawVerticalHistogramF() {
        let lineChartPadding = this.lineChart.PADDING;
        let width = 1000 - 2 * lineChartPadding;
        let height = 1000 - 2 * lineChartPadding;
        
        let yMin = this.lineChart.yMin;
        let yMax = this.lineChart.yMax;

        let dist = this.computeDistF(this.data);
		

        let g = this.lineChart._graphBitmap.getContext('2d');

        // Draw the vertical Y-axis
        this.drawLine(g,lineChartPadding + width, lineChartPadding + height, lineChartPadding + width, lineChartPadding);

        let numIntervallo = 0;
        for (let i = yMin + (yMax - yMin) / this.k; i < yMax; i += (yMax - yMin) / this.k) {
            // Numbers
            let y = lineChartPadding + height - (i - yMin) * height / (yMax - yMin);
            this.drawLine(g,lineChartPadding + width - 5, y, lineChartPadding + width + 5, y);
            g.fillText(i.toString(), lineChartPadding + width + 5, y - 5);

            // Draw K rectangles
            let rect_len = (dist[numIntervallo] / Math.max(...dist)) * height;
            let interval_len = height / this.k;
            let x1 = lineChartPadding + width - rect_len;
            let y1 = lineChartPadding + height - (i - yMin) * height / (yMax - yMin);
            numIntervallo++;
			
			g.fillStyle = 'rgba(0, 0, 255, 0.3)'; // Blue with 30% opacity
			g.fillRect(x1, y1, rect_len, interval_len);
        }
    }
	
	computeDistF(systemsFinalValues) {
        let dist = Array(this.k).fill(0);
        let lenInterval = this.lineChart.yMax / this.k;
        let j = 0;
		
        for (let numIntervallo = 0; numIntervallo < this.k; numIntervallo++) {
            for (let key in systemsFinalValues) {
                if (systemsFinalValues[key] >= j && systemsFinalValues[key] < j + lenInterval) {
                    dist[numIntervallo]++;
                }
            }
            j += lenInterval;
        }
        return dist;
    }
}

