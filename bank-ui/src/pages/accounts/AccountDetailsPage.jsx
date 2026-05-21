import {useParams} from 'react-router-dom';
import {useEffect, useState} from 'react';
import CardDetailsPage from "../../components/ui/Card/CardDetailsPage.jsx";
import TableCard from "../../components/ui/Card/Card.jsx";
import TableColumn from "../../components/ui/Card/TableColumn.jsx";
import InnerCard from "../../components/ui/Card/InnerCard.jsx";
import {sectionStyle, rowStyle, labelStyle, leftRowStyle, valueStyle} from "../../styles/accountDetailsStyle.js";
import {getAccountById, updateAccountPlan, updateAccountStatus} from "../../services/accountService.js";
import AccountSettingsCard from "../../components/forms/accounts/accountSettingsCard.jsx";
import {descriptionStyle, headerStyle} from "../../styles/detailsPageGeneralStyle.js"; 

function AccountDetails() {
    
    const {id} = useParams();
    const [account, setAccount] = useState({});
    const [isEditingPlan, setIsEditingPlan] = useState(false);
    const [selectedPlan, setSelectedPlan] = useState(0);
    
    useEffect(() => {
        
        const fetchData = async () => {
            try{
                const response = await getAccountById(id);
                setAccount(response.data);
                setSelectedPlan(response.data.plan);
            } catch (error) {
                console.log(error);
            }
        };
        fetchData();
    }, [id]);
    
    const savePlan = async () => {
        try{
            const response = await updateAccountPlan(id, selectedPlan);
            
            setAccount(response.data);
            setIsEditingPlan(false);
        }catch(error){
            console.log(error);
        }
    }
    
    const UpdateAccountStatus = async (status) => {
        
        try{
            await updateAccountStatus(id, status);
            
            setAccount(prev => ({
                ...prev,
                status
            }));
        } catch (error) {
            console.log(error.response);
        }
    }
    
    if(!account) return <div className="loader"></div>;
    
    return(
        <div style={{padding: '30px'}}>
            <CardDetailsPage>
                <h1 style={headerStyle}>
                    {account.accountId}
                </h1>
                <h2 style={descriptionStyle}>
                    Account Details
                </h2>
                <div style={{flex: 1}}>
                    <div style={{
                        display:"flex", 
                        justifyContent:"space-between"}}>
                        <InnerCard>
                        <div style={sectionStyle}>
                            <div style={leftRowStyle}>
                                <span style={labelStyle}>Account number</span>
                                <span style={valueStyle}>{account.accountNumber}</span>
                            </div>
                            
                            <div style={leftRowStyle}>
                                <span style={labelStyle}>Balance</span>
                                <span style={valueStyle}>{account.balance}</span>
                            </div>
                            
                            <div style={leftRowStyle}>
                                <span style={labelStyle}>Created At</span>
                                <span style={valueStyle}>{new Date(account.createdAt).toLocaleString()}</span>
                            </div>
                        </div>
                        </InnerCard>
                        <AccountSettingsCard
                        account={account}
                        isEditingPlan={isEditingPlan}
                        selectedPlan={selectedPlan}
                        setSelectedPlan={setSelectedPlan}
                        savePlan={savePlan}
                        setIsEditingPlan={setIsEditingPlan}
                        updateAccountStatus={UpdateAccountStatus}
                        />
                    </div>
                </div>
            </CardDetailsPage>
            <TableCard>
                <h1 style={{
                    textAlign: "center",
                    fontSize: '32px',
                    fontWeight: '400',
                    marginBottom: '20px'
                }}>
                    Connected Accounts
                </h1>
                <table 
                    cellPadding="5" 
                    style={{width:"100%", 
                        marginTop:"30px", 
                        justifyContent:"center"
                }}
                >
                    <thead>
                    <tr>
                        <TableColumn>ID</TableColumn>
                        <TableColumn>Name</TableColumn>
                        <TableColumn>Email</TableColumn>
                        <TableColumn>Phone number</TableColumn>
                        <TableColumn>Status</TableColumn>
                        <TableColumn>Created date</TableColumn>
                    </tr>
                    </thead>
                </table>
            </TableCard>
        </div>
    )
}

export default AccountDetails;