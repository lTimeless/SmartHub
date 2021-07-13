import React, {useState} from 'react'
import {
  Route,
  RouteChildrenProps,
  Switch,
} from 'react-router-dom'
import {routes} from './config/routes';

function App() {
  const [count, setCount] = useState(0)

  return (
      <Switch>
        {routes.map((route, index) => {
          return (
              <Route key={index}
                     exact={route.exact}
                     path={route.path}
                     render={(routeProps: RouteChildrenProps<any>) => <route.component {...routeProps} />}
              />
          )
        })}
      </Switch>
  )
}

export default App
