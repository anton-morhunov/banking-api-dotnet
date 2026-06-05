export const modalOverlay = {
    position: "fixed",
    inset: "0",
    background: "rgba(0, 0, 0, 0.5)",
    display: "flex",
    alignItems: "center",
    justifyContent: "center",
    zIndex: "1000"
}

export const modal = {
    background: "white",
    padding: "32px",
    borderRadius: "12px",
    width: "400px",
    display: "flex",
    flexDirection: "column",
    gap: "16px",
    boxShadow: "0 20px 60px rgba(0,0,0,0.3)"
}

export const modalButtons= {
    display: "flex",
    justifyContent: "flex-end",
    gap: "8px"
}

export const modalHeaderStyle = {
    margin: 0, 
    color: '#1e293b'
}

export const modalTextStyle = {
    padding: '10px',
    borderRadius: '6px',
    border: '1px solid #cbd5e1',
    background: 'white',
    color: '#1e293b',
    resize: 'vertical',
    minHeight: '100px',
    fontSize: '14px'
}

export const modalInputStyle = {
    padding: '10px',
    borderRadius: '6px',
    border: '1px solid #cbd5e1',
    background: 'white',
    color: '#1e293b',
    fontSize: '14px'
}

export const modalCancelButtonStyle = {
    display: 'flex', 
    justifyContent: 'flex-end', 
    gap: '8px'
}

export const modalConfirmButtonStyle = {
    background: '#2563eb', 
    color: 'white', 
    border: 'none', 
    padding: '8px 16px', 
    borderRadius: '6px', 
    cursor: 'pointer'
}