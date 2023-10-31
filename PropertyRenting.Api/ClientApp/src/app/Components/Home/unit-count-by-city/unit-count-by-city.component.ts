import { Component, OnInit } from "@angular/core";
import { UnitService } from "../../../Services/unit.service";
import { UnitCount } from "../../../Models/unit-count";
import { Chart, registerables } from "node_modules/chart.js";
Chart.register(...registerables);
@Component({
    selector: "app-unit-count-by-city",
    templateUrl: "./unit-count-by-city.component.html",
    styleUrls: ["./unit-count-by-city.component.css"],
})
export class UnitCountByCityComponent implements OnInit {
    chartData: UnitCount[] = [];
    constructor(private unitService: UnitService) {}

    ngOnInit(): void {
        this.unitService.GetCountByCity().subscribe({
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
                        "unitCountByCityChart"
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
