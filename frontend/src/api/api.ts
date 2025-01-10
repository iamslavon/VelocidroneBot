import { client, getApiDashboard } from "./client";

class ApiClient {
  constructor() {
    client.setConfig({
      // set default base url for requests
      baseUrl: import.meta.env.VITE_API_URL,
      // set default headers for requests
      headers: {
        //Authorization: "Bearer <token_from_service_client>",
      },
    });
  }

  getDashboard() {
    return getApiDashboard();
  }
}

export default new ApiClient();
