import { Component, OnInit } from "@angular/core";
import { Chart, registerables } from "node_modules/chart.js";
import { BuildingService } from "../../../Services/building.service";
import { RentedUnitsPercentage } from "../../../Models/rented-units-percentage";
Chart.register(...registerables);

@Component({
    selector: "app-rented-units-percentage",
    templateUrl: "./rented-units-percentage.component.html",
    styleUrls: ["./rented-units-percentage.component.css"],
})
export class RentedUnitsPercentageComponent implements OnInit {
    chartData: RentedUnitsPercentage[] = [];
    constructor(private buildingService: BuildingService) {}

    ngOnInit(): void {
        this.buildingService.GetRentedUnitsPercentage().subscribe({
            next: (res) => {
                this.chartData = res;
                const labelData = [];
                const realData = [];
                if (this.chartData !== null) {
                    for (let i = 0; i < this.chartData.length; i++) {
                        labelData.push(this.chartData[i].building);
                        realData.push(this.chartData[i].percentage);
                    }
                    this.RenderChart(
                        labelData,
                        realData,
                        "bar",
                        "rentedUnitsPercentageChart"
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
                        label: "النسبة المؤجرة",
                    },
                ],
            },
        });
    }
}
