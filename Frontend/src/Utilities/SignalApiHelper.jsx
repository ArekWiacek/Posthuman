const developmentApi = "https://localhost:7201/notifications";
const productionApi = "http://posthumanbackapp-001-site1.btempurl.com/notifications";
const ApiUrl = (process.env.NODE_ENV === "development") ? developmentApi : productionApi;

const SignalApi = {
    GetUrl: () => {
        return ApiUrl;
    }
}

export default SignalApi;