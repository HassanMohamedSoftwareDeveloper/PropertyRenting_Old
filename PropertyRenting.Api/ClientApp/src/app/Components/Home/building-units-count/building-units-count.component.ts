import { Component, OnInit } from "@angular/core";
import { Chart, registerables } from "node_modules/chart.js";
import { BuildingService } from "../../../Services/building.service";
import { BuildingUnitsCount } from "../../../Models/building-units-count";
Chart.register(...registerables);

@Component({
    selector: "app-building-units-count",
    templateUrl: "./building-units-count.component.html",
    styleUrls: ["./building-units-count.component.css"],
})
export class BuildingUnitsCountComponent implements OnInit {
    chartData: BuildingUnitsCount[] = [];
    constructor(private buildingService: BuildingService) {}

    ngOnInit(): void {
        this.buildingService.GetUnitsCount().subscribe({
            next: (res) => {
                this.chartData = res;
                const labelData = [];
                const realData = [];
                const realData1 = [];
                const realData2 = [];
                if (this.chartData !== null) {
                    for (let i = 0; i < this.chartData.length; i++) {
                        labelData.push(this.chartData[i].building);
                        realData.push(this.chartData[i].total);
                        realData1.push(this.chartData[i].rented);
                        realData2.push(this.chartData[i].available);
                    }
                    this.RenderChart(
                        labelData,
                        realData,
                        realData1,
                        realData2,
                        "bar",
                        "buildingUnitsCountChart"
                    );
                }
            },
            error: (error) => console.log(error),
        });
    }

    RenderChart(
        labeldata: any,
        maindata: any,
        maindata1: any,
        maindata2: any,
        type: any,
        id: any
    ) {
        new Chart(id, {
            type: type,
            data: {
                labels: labeldata,
                datasets: [
                    {
                        data: maindata,
                        borderWidth: 1,
                        label: "الإجمالى",
                    },
                    {
                        data: maindata1,
                        borderWidth: 1,
                        label: "المؤجر",
                    },
                    {
                        data: maindata2,
                        borderWidth: 1,
                        label: "المتاح",
                    },
                ],
            },
        });
    }
}
