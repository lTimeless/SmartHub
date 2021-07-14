import React from 'react';
import { BrowserRouter, Redirect, Route, Switch } from 'react-router-dom';
import AuthLayout from 'src/components/layouts/AuthLayout';
import Layout from 'src/components/layouts/Layout';
import NotFoundPage from 'src/pages/errors/NotFoundPage';
import { protectedRoutes, publicRoutes } from './config/routes';
import { Routes } from './types/enums';

const App = () => {
  return (
              <BrowserRouter>
                <Switch>
                  <Redirect exact from={Routes.Layout} to={Routes.Home} />
                  <Route exact path={[...publicRoutes.map(x => x.path)]}>
                    <AuthLayout>
                      <Switch>
                        {publicRoutes.map(route => (
                          <Route
                            key={route.name}
                            path={route.path}
                            component={route.component}
                          />
                        ))}
                      </Switch>
                    </AuthLayout>
                  </Route>
        {/* TODO make protected */}
        <Route exact path={protectedRoutes.map(x => x.path)}>
          <Layout>
            <Switch>
              {protectedRoutes.map(route => (
                <Route
                  key={route.name}
                  path={route.path}
                  component={route.component}
                />
              ))}
            </Switch>
          </Layout>
        </Route>
        <Route path='*' component={NotFoundPage} />
      </Switch>
    </BrowserRouter>
  );
};

export default App;
