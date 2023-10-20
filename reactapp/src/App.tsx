import React, { Component } from 'react';

interface WeatherForecast {
    date: string;
    temperatureC: number;
    temperatureF: number;
    summary: string;
}

interface AppState {
    loading: boolean;
    forecasts: WeatherForecast[];
}

interface AppProps {

}

export default class App extends Component<AppProps, AppState>
{
    static displayName: string = App.name;
    private static appInitState: AppState = { loading: true, forecasts: [] };

    constructor(props: AppProps) {
        super(props);

        this.state = App.appInitState;
    }

    componentDidMount(): void {
        this.populateWeatherData();
    }

    renderForecastsTable(forecasts: WeatherForecast[]): React.JSX.Element {
        return (
            <table className='table table-striped' aria-labelledby="tabelLabel">
                <thead>
                    <tr>
                        <th>Index</th>
                        <th>Date</th>
                        <th>Temp. (C)</th>
                        <th>Temp. (F)</th>
                        <th>Summary</th>
                    </tr>
                </thead>
                <tbody>
                    {forecasts.map((forecast, index) =>
                        <tr key={forecast.date}>
                            <td>{index}</td>
                            <td>{forecast.date}</td>
                            <td>{forecast.temperatureC}</td>
                            <td>{forecast.temperatureF}</td>
                            <td>{forecast.summary}</td>
                        </tr>
                    )}
                </tbody>
            </table>
        );
    }

    render(): React.JSX.Element {
        const contents = this.state.loading
            ? <p><em>Loading... Please refresh once the ASP.NET backend has started. See <a href="https://aka.ms/jspsintegrationreact">https://aka.ms/jspsintegrationreact</a> for more details.</em></p>
            : this.renderForecastsTable(this.state.forecasts);

        return (
            <div>
                <h1 id="tabelLabel">Weather forecast</h1>
                <p>This component demonstrates fetching data from the server.</p>
                {contents}
            </div>
        );
    }

    async populateWeatherData() {
        const response = await fetch('api/weatherforecast');
        const data = await response.json();
        this.setState({ forecasts: data, loading: false });
    }
}