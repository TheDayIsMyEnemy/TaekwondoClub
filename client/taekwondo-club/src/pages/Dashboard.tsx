import { useEffect, useState } from "react";
import { getMemberships } from "../api/requests";
import { Membership } from "../types";
import { Chart as ChartJS, ArcElement, Tooltip, Legend } from "chart.js";
import { Doughnut } from "react-chartjs-2";

export const Dashboard = () => {
  const [memberships, setMemberships] = useState<Membership[]>([]);
  const [isLoading, setIsLoading] = useState<boolean>(false);

  useEffect(() => {
    if (!isLoading) {
      loadMemberships();
    }
    return () => {
      setMemberships([]);
    };
  }, []);

  const loadMemberships = () => {
    setIsLoading(true);
    getMemberships().then((res) => {
      console.log(res.data);
      setMemberships(res.data);
      setIsLoading(false);
    });
  };

  const activeMemberships = memberships.filter((m) => m.isActive).length;
  const inactiveMemberships = memberships.length - activeMemberships;
  ChartJS.register(ArcElement, Tooltip, Legend);

  const data = {
    labels: ["Inactive", "Active"],
    datasets: [
      {
        label: "# of Memberships",
        data: [activeMemberships, inactiveMemberships],
        backgroundColor: [
          "rgba(255, 99, 132, 0.2)",
          "rgba(54, 162, 235, 0.2)",
          "rgba(255, 206, 86, 0.2)",
          "rgba(75, 192, 192, 0.2)",
          "rgba(153, 102, 255, 0.2)",
          "rgba(255, 159, 64, 0.2)",
        ],
        borderColor: [
          "rgba(255, 99, 132, 1)",
          "rgba(54, 162, 235, 1)",
          "rgba(255, 206, 86, 1)",
          "rgba(75, 192, 192, 1)",
          "rgba(153, 102, 255, 1)",
          "rgba(255, 159, 64, 1)",
        ],
        borderWidth: 1,
      },
    ],
  };
  return (
    <div style={{ width: 300 }}>
      <Doughnut
        // options={...}
        data={data}
        // {...props}
      />
    </div>
  );
};
