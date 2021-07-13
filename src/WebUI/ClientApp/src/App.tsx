import React from 'react'
import {
  BrowserRouter,
  Redirect,
  Route,
  Switch,
} from 'react-router-dom'
import AuthLayout from '@/components/layouts/AuthLayout';
import Layout from '@/components/layouts/Layout';
import { protectedRoutes, publicRoutes } from './config/routes';
import NotFoundPage from '@/pages/errors/NotFoundPage';
import { Routes } from './types/enums';

const App =() => {
  return (
    <BrowserRouter >
      <Switch>
        <Redirect exact from={Routes.Layout} to={Routes.Home} />
        <Route exact path={[...publicRoutes.map(x => x.path)]}>
          <AuthLayout>
            <Switch>
              {publicRoutes.map((route, i) => (
                <Route key={i} path={route.path} component={route.component} />
              ))}
            </Switch>
          </AuthLayout>
        </Route>
        {/* TODO make protected */}
        <Route exact path={protectedRoutes.map(x => x.path)}>
          <Layout>
            <Switch>
              {protectedRoutes.map((route, i) => (
                <Route key={i} path={route.path} component={route.component} />
              ))}
            </Switch>
          </Layout>
        </Route>
        <Route path="*" component={NotFoundPage} />
      </Switch>
    </BrowserRouter>
  )
}

export default App;