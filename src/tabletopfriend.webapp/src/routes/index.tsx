import { Routes, Route, Navigate } from "react-router-dom"
import { HomePage } from "../views/home/HomePage"

export const AppRoutes = () => {
    return (
        <Routes>
            <Route path="*" element={<Navigate to="/home" />} />
            <Route path="/home" element={<HomePage/>}/>
        </Routes>
    )
}