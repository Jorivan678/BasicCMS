import React, { useState } from "react";

interface LandingHomeState
{

}

export function LandingHome(): React.JSX.Element
{
    const [state, setState] = useState<LandingHomeState>({});

    return (
        <div className="container">
            <h1 className="strong">AAAAAAAAAAAAAAAAAAAAAAA</h1>
        </div>
    );
}