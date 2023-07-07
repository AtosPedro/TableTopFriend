import { Routes, Route, Navigate } from "react-router-dom"
import { HomePage } from "../views/home/HomePage"
import { LandingPage } from "../views/landing/LandingPage"
import { LandingCampaignInfoPage } from "../views/landing/LandingCampaignInfoPage"
import { LandingCharacterInfoPage } from "../views/landing/LandingCharacterInfoPage"
import { LandingGameplayInfoPage } from "../views/landing/LandingGameplayInfoPage"
import { LoginPage, RegisterPage } from "../views/authentication"

export const AppRoutes = () => {
    return (
        <Routes>
            <Route path="*" element={<Navigate to="/home" />} />
            <Route path="/" element={<LandingPage/>} />
            <Route path="/how-campaings-work" element={<LandingCampaignInfoPage/>} />
            <Route path="/how-characters-work" element={<LandingCharacterInfoPage/>} />
            <Route path="/how-gameplay-work" element={<LandingGameplayInfoPage/>} />
            <Route path="/home" element={<HomePage/>}/>
            <Route path="/login" element={<LoginPage/>}/>
            <Route path="/register" element={<RegisterPage/>}/>
        </Routes>
    )
}