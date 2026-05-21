import {labelStyle, rowStyle, selectStyle, valueStyle} from "../../../styles/accountDetailsStyle.js";
import {accountPlan, accountType} from "../../../constants/accountConstants.js";
import PrimaryButton from "../../ui/Button/Button.jsx";
import {clientStatusMap} from "../../../constants/clientConstants.js";
import AccountActions from "./AccountActions.jsx";
import InnerCard from "../../ui/Card/InnerCard.jsx";

function AccountSettingsCard(
    {
        account, 
        isEditingPlan, 
        selectedPlan, 
        setSelectedPlan, 
        savePlan, 
        setIsEditingPlan, 
        updateAccountStatus
}) {
    return (

        <InnerCard>
            <div>
                <div style={rowStyle}>
                    <span style={labelStyle}>
                        Account Plan
                    </span>
                    {
                        isEditingPlan ? (
                            <select
                                value={selectedPlan}
                                onChange={
                                    (e) =>
                                        setSelectedPlan(Number(e.target.value))}
                                style={selectStyle}
                            >
                                <option value={0}>Basic</option>
                                <option value={1}>Premium</option>
                                <option value={2}>Business</option>
                            </select>
                        ) : (
                            <span style={{
                                color: account.plan === 0
                                    ? "#64748b"
                                    : account.plan === 1
                                        ? "#eab308"
                                        : "#0f766e",
                                fontWeight: "bold"
                            }}
                            >
                                {accountPlan[account.plan]}
                            </span>
                        )
                    }
                    <PrimaryButton
                        type="button"
                        onClick={ isEditingPlan ? savePlan : () => setIsEditingPlan(true)  }
                    >
                        {isEditingPlan ? "Save" : "Edit"}
                    </PrimaryButton>
                </div>
                <div style={rowStyle}>
                    <span style={labelStyle}>Account Type</span>
                    <span style={valueStyle}>
                                    <span style={{
                                        color: account.accountType === 0
                                            ? "#2563eb"
                                            : account.accountType === 1
                                                ? "#22c55e"
                                                : "#9333ea",
                                        fontWeight: "bold"
                                    }}>{accountType[account.accountType]}
                                        </span>
                                </span>
                    <PrimaryButton
                        type="button"
                    >
                        Edit
                    </PrimaryButton>

                    <div style={rowStyle}>
                        <span style={labelStyle}>Status</span>
                        <span
                            style={{
                                color: account.status === 0 ? "green" : "red",
                                fontWeight: "bold"
                            }}
                        >
                            {clientStatusMap[account.status]}
                        </span>
                    </div>
                </div>
                <AccountActions
                    status={account.status}
                    onChangeStatus={updateAccountStatus}
                />
            </div>
        </InnerCard>
    )
}
export default AccountSettingsCard;