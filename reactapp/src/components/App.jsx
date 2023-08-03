import React from 'react';
import { BrowserRouter as Router, Route, Routes} from "react-router-dom";
import Home from "./Pages/Home";
import Register from "./Pages/Auth/Register";
import Login from "./Pages/Auth/Login";
import Logout from "./Pages/Auth/Logout";
import NotFound from "./Pages/NotFound";
import Words from "./Pages/Learner/Words";
import Sentences from "./Pages/Learner/Sentences";
import Idioms from "./Pages/Learner/Idioms";
import Rules from "./Pages/Learner/Rules";
import GenerateSentences from "./Pages/Generators/GenerateSentences";
import GenerateIdioms from "./Pages/Generators/GenerateIdioms";
import UserDetails from "./Pages/Account/User/UserDetails";
import EditUserPassword from "./Pages/Account/User/EditUserPassword";
import Users from "./Pages/Account/Admin/Users";
import Roles from "./Pages/Account/Admin/Roles";
import Languages from "./Pages/Account/Admin/Languages";
import Nav  from './Elements/Nav';
import Header from './Elements/Header';
import UserWrapper from './Wrappers/UserWrapper';
import AdminWrapper from './Wrappers/AdminWrapper';
import {LanguageProvider} from './Providers/LanguageProvider';


export default function App(){
  return  <React.StrictMode>
        <LanguageProvider>
            <Router>    
                <div className="container">
                    <Header />
                    <Nav />
                    <br />
                    <Routes>
                        <Route element={<UserWrapper />}>
                            <Route index element={<Home />} />
                            <Route path='/logout' element={<Logout />} />
                            <Route path='/words' element={<Words />} />
                            <Route path='/sentences' element={<Sentences />} />
                            <Route path='/sentences/generate' element={<GenerateSentences />} />
                            <Route path='/idioms' element={<Idioms />} />
                            <Route path='/idioms/generate' element={<GenerateIdioms />} />
                            <Route path='/rules' element={<Rules />} />
                            <Route path='/user-details' element={<UserDetails />} />
                            <Route path='/user/edit-password' element={<EditUserPassword />} />
                        </Route>
                        <Route element={<AdminWrapper />}>
                            <Route path='/users' element={<Users />} />
                            <Route path='/roles' element={<Roles />} />
                            <Route path='/languages' element={<Languages />} />
                        </Route>
                        <Route path='/register' element={<Register />} />
                        <Route path='/login' element={<Login />} />
                        <Route path='*' element={<NotFound />} /> 
                    </Routes>
                </div>
            </Router>
        </LanguageProvider>
    </React.StrictMode>;
}