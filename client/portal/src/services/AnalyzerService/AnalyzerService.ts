const baseUrl = "https://localhost:7268";

const routes = {
  analyzePath: "analyze",
};

const analyzePath = async (map: string[]) => {
  console.log(map);
  const response = await fetch(`${baseUrl}/${routes.analyzePath}`, {
    method: "POST",
    headers: {
      "Content-Type": "application/json; charset=UTF-8",
    },
    body: JSON.stringify(map),
  });

  if (!response.ok) {
    throw new Error("Could not analyze path.");
  }

  const data = await response.json();

  return data;
};

export const analyzeService = {
  analyzePath,
};
