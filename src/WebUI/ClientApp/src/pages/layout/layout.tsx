import {inAppRoutes} from '@/config/routes'
import {Routes} from '@/types/enums'
import React from 'react'
import {Link, Route, Switch} from 'react-router-dom'

interface LayoutProps {
}

const Layout: React.FC<LayoutProps> = props => {
    return (
        <div className="bg-burntSienna-200 h-screen">
            Layout page
            <ul style={{listStyleType: "none", padding: 0}}>
                <li>
                    <Link to={Routes.Home}>Home</Link>
                </li>
                <li>
                    <Link to={Routes.Devices}>Devices</Link>
                </li>
            </ul>
            <div style={{flex: 1, padding: "10px"}}>
                <Switch>
                    {inAppRoutes.map((route, index) => (
                        <Route
                            key={index}
                            path={route.path}
                            exact={route.exact}
                            children={<route.component/>}
                        />
                    ))}
                </Switch>
            </div>
        </div>
    )
}

export default Layout
