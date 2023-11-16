class Adversary {
    // Attributes
    constructor(m, n, p) {
        this.N = n;
        this.M = m;
        this.P = p;
        this.lineChartValuesChart2 = [];
        this.generateAttacks();
    }

    // Method to generate attacks
    generateAttacks() {
        for (let i = 0; i < this.numberOfSystem; i++) {
            const valuesChart2 = [];

            for (let j = 0; j < this.numberOfAttacks; j++) {
                const randomNumber = Math.random();

                if (randomNumber > this.probability) {
                    // Attack success
                    console.log(`System ${i}: Attack ${j} succeeded`);
                    valuesChart2.push(1);
                } else {
                    // Attack failed
                    console.log(`System ${i}: Attack ${j} failed`);
                    valuesChart2.push(0);
                }
            }
            this.lineChartValuesChart2.push(valuesChart2);
        }
    }


    createCompleteHistogramData(values, S) {
        const list = [];

        for (let k = 2; k <= 10; k++) {
            list.push(this.createHistoDistrib(values, S, k * 10));
        }

        return list;
    }

    // Returns a dictionary indicating whether each system is safe or not for a given S and P
    createHistoDistrib(values, S, P) {
        const finalValues = {};

        for (let i = 0; i < values.length; i++) {
            let sum = 0;
            for (let s = 0; s < values[i].length; s++) {
                sum += values[i][s];
                // Check if I reached S or P
                if (sum === P) {
                    // System is not safe
                    finalValues[i] = 0;
                    // Exit the loop
                    break;
                } else if (sum === S) {
                    // System is safe
                    finalValues[i] = 1;
                    // Exit the loop
                    break;
                }
            }
            // console.log("System" + i + " final value = " + sum);
        }

        return finalValues;
    }

    getLineChart2AttackList() {
        return this.lineChartValuesChart2;
    }

}

