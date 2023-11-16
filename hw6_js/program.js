function simulateDiscardProbability(A, B, C, D) {
    let discardedCount = 0;

    for (let i = 0; i < A; i++) {
        let penetrationScoreVariable
         = 0;

        for (let j = 0; j < B; j++) {
            penetrationScoreVariable += Math.random() * 100; 

            if (penetrationScoreVariable >= C) {
                break; 
            }

            if (penetrationScoreVariable < D) {
                discardedCount++;
                break; //  discard
            }
        }
    }

    return discardedCount / A; // Probability of being discarded
}

function SimulationRun() {
    const A = 10000; //  systems number
    const B = 10;    // attacks number

    const D_values = [20, 60, 100]; // Different values of D

    for (let k = 2; k <= 10; k++) {
        const C = k * 10;

        for (let i = 0; i < D_values.length; i++) {
            const D = D_values[i];

            const probability = simulateDiscardProbability(A, B, C, D);

            console.log(`For C = ${C} and D = ${D}, Probability of being discarded: ${probability}`);
        }
    }
}

SimulationRun();
