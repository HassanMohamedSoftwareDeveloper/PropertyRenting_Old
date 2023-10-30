import { Component, OnInit } from "@angular/core";
import { Chart, registerables } from "node_modules/chart.js";
import { BuildingService } from "../../../Services/building.service";
import { BuildingCount } from "../../../Models/building-count";
Chart.register(...registerables);

@Component({
    selector: "app-building-count-by-city",
    templateUrl: "./building-count-by-city.component.html",
    styleUrls: ["./building-count-by-city.component.css"],
})
export class BuildingCountByCityComponent implements OnInit {
    chartData: BuildingCount[] = [];
    constructor(private buildingService: BuildingService) {}

    ngOnInit(): void {
        this.buildingService.GetCountByCity().subscribe({
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
                        "cityChart"
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
            options: {
                scales: {
                    y: {
                        beginAtZero: true,
                    },
                },
            },
        });
    }
}
