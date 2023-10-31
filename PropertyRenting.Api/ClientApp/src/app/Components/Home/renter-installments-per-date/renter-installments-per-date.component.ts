import { Component, OnInit } from "@angular/core";
import { Chart, registerables } from "node_modules/chart.js";
import { RenterContractService } from "../../../Services/renter-contract.service";
import { InstallmentsPerDate } from "../../../Models/installments-per-date";
Chart.register(...registerables);

@Component({
    selector: "app-renter-installments-per-date",
    templateUrl: "./renter-installments-per-date.component.html",
    styleUrls: ["./renter-installments-per-date.component.css"],
})
export class RenterInstallmentsPerDateComponent implements OnInit {
    chartData: InstallmentsPerDate[] = [];
    constructor(private renterContractService: RenterContractService) {}

    ngOnInit(): void {
        this.renterContractService.GetInstallmentsPerDate().subscribe({
            next: (res) => {
                this.chartData = res;
                const labelData = [];
                const realData = [];
                const realData1 = [];
                if (this.chartData !== null) {
                    for (let i = 0; i < this.chartData.length; i++) {
                        labelData.push(this.chartData[i].dueDate);
                        realData.push(this.chartData[i].total);
                        realData1.push(this.chartData[i].count);
                    }
                    this.RenderChart(
                        labelData,
                        realData,
                        realData1,
                        "line",
                        "renterInstallmentsPerDateChart"
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
                        label: "إجمالى الأقساط",
                        pointStyle: "circle",
                        pointRadius: 10,
                        pointHoverRadius: 15,
                        yAxisID: "y",
                    },
                    {
                        data: maindata1,
                        borderWidth: 1,
                        label: "عدد الأقساط",
                        pointStyle: "rectRounded",
                        pointRadius: 10,
                        pointHoverRadius: 15,
                        yAxisID: "y1",
                    },
                ],
            },
            options: {
                interaction: {
                    intersect: false,
                    mode: "index",
                },
                scales: {
                    x: {
                        display: true,
                        title: {
                            display: true,
                            text: "تاريخ الإستحقاق",
                        },
                    },
                    y: {
                        type: "linear",
                        display: true,
                        position: "left",
                        title: {
                            display: true,
                            text: "الإجمالى",
                        },
                    },
                    y1: {
                        type: "linear",
                        display: true,
                        position: "right",
                        title: {
                            display: true,
                            text: "العدد",
                        },
                        // grid line settings
                        grid: {
                            drawOnChartArea: false, // only want the grid lines for one axis to show up
                        },
                        //beginAtZero: true,
                    },
                },
            },
        });
    }
}
