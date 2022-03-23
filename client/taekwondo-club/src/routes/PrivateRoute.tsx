import { useEffect } from 'react'
import { Navigate } from 'react-router-dom'

const useAuth = () => {
  useEffect(() => {
    alert("You're not authenticated")
  }, [])
  return false
}

export default function PrivateRoute(props: any) {
  const auth = useAuth()
  return auth ? props.children : <Navigate to="/" />
}
