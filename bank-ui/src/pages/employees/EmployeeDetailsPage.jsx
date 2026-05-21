import {useEffect, useState} from "react";
import {getEmployeeById} from "../../services/employeeService.js";
import {useParams} from "react-router-dom";
import CardDetailsPage from "../../components/ui/Card/CardDetailsPage.jsx";
import InnerCard from "../../components/ui/Card/InnerCard.jsx";
import {rowStyle, sectionStyle, labelStyle} from "../../styles/employeeDetailsStyle.js";
import {valueStyle} from "../../styles/clientDetailsStyles.js";
import {roleMap} from "../../constants/employeeConstants.js";
import {descriptionStyle, headerStyle} from "../../styles/detailsPageGeneralStyle.js";

function EmployeeDetailsPage(){
    
    const { id } = useParams()
    const [employee, setEmployee] = useState(null);
    useEffect(() => {
        const fetchEmployee = async () => {
            try {
                const response = await getEmployeeById(id);
                setEmployee(response.data);
            }catch(err){
                console.log(err);
            }
        };
        fetchEmployee();
    }, [id]);

    if (!employee) return <div className="loader"></div>;
    
    return (
        <div style={{ padding: "30px" }}>
            <CardDetailsPage>
                <h1 style ={headerStyle}
                >
                    {employee.id}
                </h1>
                <h2 
                    style={descriptionStyle}>
                    Employee Details
                </h2>
                <div
                    style={{
                        display: "flex", 
                        alignItems: "flex-start", 
                        justifyContent: "space-between"
                }}
                >
                    <div style={{ flex: 1 }}>
                        <div style={{
                            display: "flex",
                            justifyContent: "space-between"
                        }}
                        >
                           <InnerCard>
                               <div style={sectionStyle}>
                                   <div style={rowStyle}>
                                       <span style={labelStyle}> Email </span>
                                       <span style={valueStyle}>{employee.email}</span>
                                   </div>
                                   <div style={rowStyle}>
                                       <span style={labelStyle}> User Role </span>
                                       <span style={{
                                           color: employee.userRole === 0 ? "green" : "red", 
                                           fontWeight: "bold"
                                       }}
                                       >
                                           {roleMap[employee.userRole]}
                                       </span>
                                   </div>
                               </div>
                           </InnerCard> 
                        </div>
                    </div>
                </div>
            </CardDetailsPage>
        </div>
    )
}

export default EmployeeDetailsPage;