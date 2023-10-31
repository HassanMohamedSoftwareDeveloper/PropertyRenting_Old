import { Component, OnInit } from "@angular/core";
import { Chart, registerables } from "node_modules/chart.js";
import { RenterContractService } from "../../../Services/renter-contract.service";
import { ContractCount } from "../../../Models/contract-count";
Chart.register(...registerables);

@Component({
    selector: "app-renter-contract-count-by-state",
    templateUrl: "./renter-contract-count-by-state.component.html",
    styleUrls: ["./renter-contract-count-by-state.component.css"],
})
export class RenterContractCountByStateComponent implements OnInit {
    chartData: ContractCount[] = [];
    constructor(private renterContractService: RenterContractService) {}

    ngOnInit(): void {
        this.renterContractService.GetCountByState().subscribe({
            next: (res) => {
                this.chartData = res;
                const labelData = [];
                const realData = [];
                if (this.chartData !== null) {
                    for (let i = 0; i < this.chartData.length; i++) {
                        labelData.push(this.chartData[i].description);
                        realData.push(this.chartData[i].count);
                    }
                    this.RenderChart(
                        labelData,
                        realData,
                        "doughnut",
                        "renterContractCountByStateChart"
                    );
                }
            },
            error: (error) => console.log(error),
        });
    }

    RenderChart(labeldata: any, maindata: any, type: any, id: any) {
        new Chart(id, {
            type: type,
            data: {
                labels: labeldata,
                datasets: [
                    {
                        data: maindata,
                        borderWidth: 1,
                    },
                ],
            },
        });
    }
}
